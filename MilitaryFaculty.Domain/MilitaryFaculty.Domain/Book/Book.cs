using System;
using System.ComponentModel;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Domain.Contract;

namespace MilitaryFaculty.Domain
{
    public class Book : UniqueEntity, IImitator<Book>
    {
        #region Class Fields

        private BookType bookType;

        #endregion // Class Fields

        #region Class Properties

        public virtual Professor Author { get; set; }
        public virtual string Name { get; set; }
        public virtual int PagesCount { get; set; }

        public virtual BookType BookType
        {
            get { return bookType; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                bookType = value;
            }
        }

        #endregion // Class Properties

        #region Class Constructors

        public Book()
        {
            Id = Guid.Empty;
            Name = String.Empty;
            PagesCount = 0;
            bookType = BookType.Schoolbook;
        }

        public Book(Book other)
        {
            Imitate(other);
        }

        #endregion // Class Constructors

        #region Class Public Methods

        public void Imitate(Book other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            Id = other.Id;
            Name = other.Name;
            PagesCount = other.PagesCount;
            Author = other.Author;
            BookType = other.BookType;
        }

        #endregion // Class Public Methods
    }
}