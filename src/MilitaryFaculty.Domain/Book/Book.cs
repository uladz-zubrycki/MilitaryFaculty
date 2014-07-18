using System;
using System.ComponentModel;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public class Book : UniqueEntity, IImitator<Book>
    {
        private BookType _bookType;

        public virtual Professor Author { get; set; }
        public virtual string Name { get; set; }
        public virtual int PagesCount { get; set; }
        public virtual DateTime Date { get; set; }

        public virtual BookType BookType
        {
            get { return _bookType; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                _bookType = value;
            }
        }

        public Book()
        {
            Name = String.Empty;
            Date = DateTime.Now;
        }

        public Book(Book other)
        {
            Imitate(other);
        }

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
            Date = other.Date;
        }
    }
}