using System;
using System.Collections.ObjectModel;
using System.Linq;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ProfessorBooksViewModel : ViewModel<Professor>
    {
        #region Class Fields

        private readonly IRepository<Book> bookRepository;
        private ObservableCollection<BookListItemViewModel> books;

        #endregion // Class Fields

        #region Class Properties

        public override string Title
        {
            get
            {
                return "Разработка учебников, учебных пособий";
            }
        }

        public ObservableCollection<BookListItemViewModel> Books
        {
            get
            {
                if (books == null)
                {
                    InitBooks();
                }

                return books;
            } 
        }

        public int SchoolbooksCount
        {
            get
            {
                return Books.Count(vm => vm.Model.BookType == BookType.Schoolbook);
            }
        }

        public int TutorialsCount
        {
            get
            {
                return Books.Count(vm => vm.Model.BookType == BookType.Tutorial);
            }
        }

        #endregion // Class Properties

        #region Class Constructors

        public ProfessorBooksViewModel(Professor model, IRepository<Book> bookRepository)
            : base(model)
        {
            if (bookRepository == null)
            {
                throw new ArgumentNullException("conferenceRepository");
            }

            this.bookRepository = bookRepository;

            bookRepository.EntityCreated += OnBookCreated;
            bookRepository.EntityDeleted += OnBookDeleted;
            Commands.Add(CreateAddBookCommand());
        }

        #endregion // Class Constructors

        #region Class Private Methods

        private ImagedCommandViewModel CreateAddBookCommand()
        {
            const string tooltip = "Добавить учебник";
            const string imageSource = @"..\Content\add.png";

            return new ImagedCommandViewModel(Browse.Book.Add,
                                              Model, tooltip, imageSource);
        }

        private void InitBooks()
        {
            var converter = BookListItemViewModel.FromModel();
            var items = Model.Books.Select(converter);

            books = new ObservableCollection<BookListItemViewModel>(items);

            books.CollectionChanged += (sender, args) =>
            {
                OnPropertyChanged("SchoolbooksCount");
                OnPropertyChanged("TutorialsCount");
            };
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

        #endregion // Class Private Methods
    }
}