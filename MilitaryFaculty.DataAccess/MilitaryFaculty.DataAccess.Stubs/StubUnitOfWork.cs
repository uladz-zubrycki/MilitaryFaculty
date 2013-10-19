using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MilitaryFaculty.DataAccess.Contract;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.DataAccess.Stubs
{
    public class StubUnitOfWork
    {
        #region Class Fields
        private static readonly Cathedra[] Cathedras = new[]
            {
                new Cathedra("Кафедра тактики"),
                new Cathedra("Кафедра связи"),
                new Cathedra("Кафедра ПВО"),
                new Cathedra("Кафедра спец. связи"),
            };

        private static readonly Professor[] Professors = new[]
            {
                new Professor
                    {
                        FullName = new FullName("Касанин", "Сергей", "Николаевич"),
                        Cathedra = Cathedras[1],
                        MilitaryRank = MilitaryRank.Colonel,
                        JobPosition = JobPosition.Professor,
                    },
                new Professor
                    {
                        FullName = new FullName("Кашкаров", "Андрей", "Васильевич"),
                        Cathedra = Cathedras[1],
                        MilitaryRank = MilitaryRank.Colonel,
                        JobPosition = JobPosition.Professor,
                    },
                new Professor
                    {
                        FullName = new FullName("Градусов", "Роман", "Анатольевич"),
                        Cathedra = Cathedras[1],
                        MilitaryRank = MilitaryRank.Colonel,
                        JobPosition = JobPosition.Professor,
                    },
                new Professor
                    {
                        FullName = new FullName("Дюжов", "Геннадий", "Юрьевич"),
                        Cathedra = Cathedras[1],
                        MilitaryRank = MilitaryRank.Colonel,
                        JobPosition = JobPosition.Professor,
                    },
            };

        private static readonly Conference[] Conferences = new[]
            {
                new Conference
                    {
                        Name = "Конференция по вопросам морально-этического содержания " +
                               "курса молодого бойца в частях специального назначения " +
                               "военно-морского десанта Республики Беларусь",
                        Date = DateTime.Parse("01.07.1993"),
                        Curator = Professors[0],
                    },
                new Conference
                    {
                        Name = "Военно-научная конференция, посвященная 56-летию Великой Победы",
                        Date = DateTime.Parse("11.09.2001"),
                        Curator = Professors[0],
                    },
                new Conference
                    {
                        Name = "Вторая международная конференция",
                        Date = DateTime.Parse("13.08.2007"),
                        Curator = Professors[0],
                    },
            };

        private static readonly Book[] Books = new[]
            {
                new Book
                    {
                        Name = "Учебник по специальной подготовке",
                        PagesCount = 123,
                        BookType = BookType.Schoolbook,
                        Author = Professors[0],
                    },
                new Book
                {
                    Name = "Учебник по тактической подготовке",
                    PagesCount = 321,
                    BookType = BookType.Schoolbook,
                    Author = Professors[0],
                },
                new Book
                {
                    Name = "Как стать младшим офицером, " +
                           "начальником радиостанций коротковолновых " +
                           "малой мощности Р142-Н, Р145-БН за 21 день " +
                           "или курс молодого связиста для чайников",
                    PagesCount = 1023,
                    BookType = BookType.Tutorial,
                    Author = Professors[0],
                },
            };
        #endregion // Class Fields

        public IRepository<Cathedra> CathedraRepository
        {
            get { return new StubRepository<Cathedra>(Cathedras); }
        }

        public IRepository<Conference> ConferenceRepository
        {
            get { return new StubRepository<Conference>(Conferences); }
        }

        public IRepository<Professor> ProfessorRepository
        {
            get { return new StubRepository<Professor>(Professors); }
        }

        public IRepository<Book> BooksRepository
        {
            get { return new StubRepository<Book>(Books); }
        }
    }
}
