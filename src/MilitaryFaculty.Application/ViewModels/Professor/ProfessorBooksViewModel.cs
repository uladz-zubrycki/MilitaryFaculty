using System;
using System.Collections.ObjectModel;
using System.Linq;
using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    public class ProfessorBooksViewModel : ViewModel<Professor>
    {
        private readonly Lazy<ObservableCollection<BookListItemViewModel>> _books;

        public ProfessorBooksViewModel(Professor model, IRepository<Book> bookRepository)
            : base(model)
        {
            if (bookRepository == null)
            {
                throw new ArgumentNullException("conferenceRepository");
            }

            _books = Lazy.Create(CreateBooksViewModel);

            bookRepository.EntityCreated += OnBookCreated;
            bookRepository.EntityDeleted += OnBookDeleted;
            Commands.Add(CreateAddBookCommand());
        }

        public override string Title
        {
            get { return "Разработка учебников, учебных пособий"; }
        }

        public ObservableCollection<BookListItemViewModel> Books
        {
            get { return _books.Value; }
        }

        public int SchoolbooksCount
        {
            get { return Books.Count(vm => vm.Model.BookType == BookType.Schoolbook); }
        }

        public int TutorialsCount
        {
            get { return Books.Count(vm => vm.Model.BookType == BookType.Tutorial); }
        }

        private ImagedCommandViewModel CreateAddBookCommand()
        {
            const string tooltip = "Добавить учебник";
            const string imageSource = @"..\Content\add.png";

            return new ImagedCommandViewModel(Browse.BookAdd,
                                              Model, tooltip, imageSource);
        }

        private ObservableCollection<BookListItemViewModel> CreateBooksViewModel()
        {
            var books = Model.Books
                             .Select(BookListItemViewModel.FromModel)
                             .ToList();

            var result = new ObservableCollection<BookListItemViewModel>(books);
            result.CollectionChanged += (sender, args) =>
                                        {
                                            //todo property name from expression
                                            OnPropertyChanged("SchoolbooksCount");
                                            OnPropertyChanged("TutorialsCount");
                                        };

            return result;
        }

        private void OnBookCreated(object sender, ModifiedEntityEventArgs<Book> e)
        {
            var book = e.ModifiedEntity;
            Books.Add(new BookListItemViewModel(book));
        }

        private void OnBookDeleted(object sender, ModifiedEntityEventArgs<Book> e)
        {
            var book = e.ModifiedEntity;
            Books.RemoveSingle(c => c.Model.Equals(book));
        }
    }
}