using ImageSearchServer.Core;
using ImageSearchServer.DataAccesslayer;
using ImageSearchServer.MVVM.Model;
using ImageSearchServer.DataAccesslayer;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ImageSearchServer.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject, IMainViewModel
    {
        private readonly IClientAppDAL _AppDAL;
        private readonly ILogger _logger;
        private const int ItemsPerPage = 50;
        private string ImageUrl = "https://www.linkedin.com/in/snehalshaevya/";

        public RelayCommand ShowSearchViewCommand { get; set; }
        public RelayCommand SearchImagesCommand { get; set; }

        public MainViewModel(IClientAppDAL appDAL, ILogger logger)
        {
            _AppDAL = appDAL;
            _logger = logger;
            _bioURL = ImageUrl;
            SetupRelayCommands();
        }

        #region ViewModel Variables
        private ObservableCollection<ImageModel> _images = new ObservableCollection<ImageModel>();
        public ObservableCollection<ImageModel> Images
        {
            get { return _images; }
            set 
            { 
                _images = value;
                OnPropertyChanged();
            }
        }

        private string _keyword = "";
        public string Keyword
        {
            get { return _keyword; }
            set { 
                _keyword = value; 
                OnPropertyChanged(); 
            }
        }


        private int _pageNo = 0;
        public int PageNo
        {
            get { return _pageNo; }
            set { 
                _pageNo = value;
                CurrentDataCount = value * ItemsPerPage;
                OnPropertyChanged();
            }
        }
        

        private int _currentDataCount;
        public int CurrentDataCount
        {
            get { return _currentDataCount; }
            set 
            { 
                _currentDataCount = value;
                OnPropertyChanged();
            }
        }

        private int _totalDataCount = 0;

        public int TotalDataCount
        {
            get { return _totalDataCount; }
            set { _totalDataCount = value; OnPropertyChanged(); }
        }

        private bool _showSearchView = false;

        public bool ShowSearchView
        {
            get { return _showSearchView; }
            set { 
                _showSearchView = value; 
                OnPropertyChanged(); 
            }
        }

        private string _searchedKeyword = "";
        public string SearchedKeyword
        {
            get { return _searchedKeyword; }
            set
            {
                _searchedKeyword = value;
                OnPropertyChanged();
            }
        }

        private string _bioURL;

        public string BioUrl
        {
            get { return _bioURL; }
            set { _bioURL = value; }
        }


        #endregion

        public void SearchImages()
        {
            PageNo = 1;
            CurrentDataCount = ItemsPerPage;
            Images = new ObservableCollection<ImageModel>(GetImages().ToList());
        }

        public void GetNextImages() {
            PageNo++;
            foreach (var image in GetImages())
            {
                Images.Add(image);
            }
        }

        private void SetupRelayCommands()
        {
            ShowSearchViewCommand = new RelayCommand(o =>
            {
                ShowSearchView = true;
                SearchImages();
            });

            SearchImagesCommand = new RelayCommand(o =>
            {
                SearchImages();
            });
        }

        private IEnumerable<ImageModel> GetImages()
        {
            IEnumerable<ImageModel> images = new List<ImageModel>();

            try
            {
                if (!string.IsNullOrEmpty(Keyword))
                {
                    SearchedKeyword = Keyword;
                    var imagesInfo = _AppDAL.GetImages(Keyword, PageNo, ItemsPerPage);
                    images = imagesInfo.Images;
                    TotalDataCount = imagesInfo.TotalResults;
                }
                else
                {
                    _logger.Debug("Keyword is empty.");
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Exception in MainViewModel, ExMsg : {ex.Message}");
            }
            return images;
        }

    }
}
