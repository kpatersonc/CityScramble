using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Caliburn.Micro.Xamarin.Forms;
using InfusionGames.CityScramble.Models;
using InfusionGames.CityScramble.Services;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace InfusionGames.CityScramble.ViewModels
{
    public class SubmitClueViewModel : BaseScreen
	{
		private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;
        private readonly IImageService _imageService;
        private readonly IMessageDialogService _messageDialogService;
	    private readonly ISettingsService _settingsService;

        private ImageSource _imageUrl;
		private ICommand _submitPhotoCommand;
		private ClueResponse _response;

		public SubmitClueViewModel(
            IDataService dataService,
            IImageService imageService,
            INavigationService navigationService,
            IMessageDialogService messageDialogService,
            ISettingsService settingsService)
        {
			_navigationService = navigationService;
            _dataService = dataService;
            _imageService = imageService;
            _messageDialogService = messageDialogService;
            _settingsService = settingsService;
            _submitPhotoCommand = new Command(e => Submit(), e => CanSubmit);
		}

		/// <summary>
        /// Navigation Parameter: Clue Id
        /// </summary>
        public string ClueId { get; set; }

		/// <summary>
        /// Navigation Parameter: Is Race Active
        /// </summary>
        public bool IsRaceActive { get; set; }

		/// <summary>
        /// Navigation Parameter: Has Response
        /// </summary>
        public bool HasResponse
        {
            get;
            set;
        }

		/// <summary>
		/// Navigation Parameter: IsChanged
		/// </summary>
		public bool IsChanged { get; set;}

        /// <summary>
        /// Navigation Parameter: ImageFile
        /// </summary>
		public MediaFile ImageFile { get; set;}

        public ClueStatus? ResponseStatus
        {
            get { return _response?.Status; }
        }

        public ImageSource ImageUrl
        {
			get { return _imageUrl; }
			set { SetField(ref _imageUrl, value);}
        }

		public ICommand SubmitCommand
		{
			get { return _submitPhotoCommand; }
		}

		public override bool IsBusy
        {
            get { return base.IsBusy; }
            set
            {
                base.IsBusy = value;
                NotifyOfPropertyChange(() => CanSubmit);
            }
        }

		public bool CanSubmit
        {
            get
            {
                // if we're waiting for something, disable submit
                if (IsBusy)
                    return false;

				// no need to submit if the image hasn't changed
				if (!IsChanged)
					return false;

                // if we don't have an image to upload, disable submit
                if (ImageUrl == null)
                    return false;

                // if we have a status that has been reviewed, disable submit
                if (ResponseStatus.HasValue && ResponseStatus.Value > ClueStatus.Pending)
                    return false;

                // we have an image that hasn't been submitted or reviewed
                return true;
            }
        }

		protected override async void OnActivate()
		{
			base.OnActivate();

		    await RefreshClue();
		}

	    private async Task RefreshClue()
	    {
            try
            {
                IsBusy = true;

                var clues = await _dataService.GetCluesForTeamAsync(_settingsService.RaceId);
                var clue = clues.FirstOrDefault(c => c.Id == ClueId);

                if (clue.HasResponse())
                {
                    _response = await _dataService.GetClueResponse(ClueId);
                    if (_response != null)
                    {
                        HasResponse = true;
                        ImageUrl = _response.Data;
                    }
                }

                if (!HasResponse || IsChanged)
                {
                    if (ImageFile != null)
                    {
                        ImageUrl = ImageSource.FromStream(() => ImageFile.GetStream());
                    }
                }

                NotifyOfPropertyChange(() => CanSubmit);

                IsBusy = false;
            }
            catch
            {
                await _messageDialogService.ShowAsync("Something went wrong", "Couldn't get the details for this clue. Try again?");
            }
        }

	    private async void Submit()
        {
            IsBusy = true;

            try
            {
				if (ImageFile != null)
                {
                    var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 50;
                    Position position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
                    byte[] resizedImage = await _imageService.CompressImageAsync(GetByteArray(ImageFile.GetStream()));
                    _response = await _dataService.PostClueResponse(ClueId, position.Latitude, position.Longitude, resizedImage, _response?.Version);
                    if (_response != null)
                    {
                        await _navigationService.GoBackAsync();
                    }
                }
            }
            catch (Exception)
            {
                IsBusy = false;
                await RefreshClue();
                await _messageDialogService.ShowAsync("Submit Error", "There was some error. Try Again.");
            }

            IsBusy = false;
        }


        private byte[] GetByteArray(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
	}
}

