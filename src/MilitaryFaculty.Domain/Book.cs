using System;
using MilitaryFaculty.Domain.Base;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public enum BookType
    {
        [EnumName("Учебник")]
        Schoolbook,

        [EnumName("Учебное пособие")]
        Tutorial
    }

    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    // Properties are virtual only for EntityFramework
    public class Book : UniqueEntity, IImitator<Book>
    {
        public virtual Professor Author { get; set; }
        public virtual string Name { get; set; }
        public virtual int PagesCount { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual BookType BookType { get; set; }
       
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
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}