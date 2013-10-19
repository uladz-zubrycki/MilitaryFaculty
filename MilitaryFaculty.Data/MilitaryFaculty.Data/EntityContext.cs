using System;
using System.Data.Entity;
using System.Linq;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    public class EntityContext : DbContext
    {
        public class DropCreateInitializeDatabase : DropCreateDatabaseAlways<EntityContext>
        {
            #region Class Protected Methods

            protected override void Seed(EntityContext context)
            {
                if (context == null)
                {
                    throw new ArgumentNullException("context");
                }

                try
                {

                    SeedCathedras(context);
                    SeedProfessors(context);
                    SeedConferences(context);
                    SeedBooks(context);

                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    
                }
            }

            #endregion // Class Protected Methods

            #region Class Private Methods

            private void SeedCathedras(EntityContext context)
            {
                var cathedras = new[]
                                {
                                    new Cathedra("Кафедра тактики"),
                                    new Cathedra("Кафедра связи"),
                                    new Cathedra("Кафедра ПВО"),
                                };

                foreach (var cathedra in cathedras)
                {
                    context.Cathedras.Add(cathedra);
                }
            }

            private void SeedProfessors(EntityContext context)
            {
                var cathedra = context.Set<Cathedra>()
                                      .AsQueryable()
                                      .Single(c => c.Name == "Кафедра связи");

                var professors = new[]
                                 {
                                     new Professor
                                     {
                                         FullName = new FullName("Сергей", "Николаевич", "Касанин"),
                                         Cathedra = cathedra,
                                         MilitaryRank = MilitaryRank.Colonel,
                                         JobPosition = JobPosition.Professor,
                                     },
                                     new Professor
                                     {
                                         FullName = new FullName("Андрей", "Васильевич", "Кашкаров"),
                                         Cathedra = cathedra,
                                         MilitaryRank = MilitaryRank.Colonel,
                                         JobPosition = JobPosition.Professor,
                                     },
                                     new Professor
                                     {
                                         FullName = new FullName("Роман", "Анатольевич", "Градусов"),
                                         Cathedra = cathedra,
                                         MilitaryRank = MilitaryRank.Colonel,
                                         JobPosition = JobPosition.Professor,
                                     },
                                     new Professor
                                     {
                                         FullName = new FullName("Дюжов", "Юрьевич", "Геннадий"),
                                         Cathedra = cathedra,
                                         MilitaryRank = MilitaryRank.Colonel,
                                         JobPosition = JobPosition.Professor,
                                     },
                                 };

                foreach (var professor in professors)
                {
                    context.Professors.Add(professor);
                }
            }

            private void SeedConferences(EntityContext context)
            {
                var professor1 = context.Set<Professor>()
                                        .AsQueryable()
                                        .Single(p => p.FullName.LastName == "Кашкаров");

                var professor2 = context.Set<Professor>()
                                        .AsQueryable()
                                        .Single(p => p.FullName.LastName == "Касанин");

                var conferences = new[]
                                  {
                                      new Conference
                                      {
                                          Name = "Конференция по вопросам морально-этического содержания " +
                                                 "курса молодого бойца в частях специального назначения " +
                                                 "военно-морского десанта Республики Беларусь",
                                          Date = DateTime.Parse("01.07.1993"),
                                          Curator = professor1,
                                          ConferenceType = ConferenceType.University,
                                          ConferenceReport = new ConferenceReport()
                                                             {
                                                                 OrganizationCorrectness = AccordanceLevel.Fully,
                                                                 ReportMaterials = AccordanceLevel.None,
                                                                 ThemeActuality = AccordanceLevel.Partly,
                                                                 ResultsUsage = AccordanceLevel.Partly
                                                             }
                                      },
                                      new Conference
                                      {
                                          Name = "Военно-научная конференция, посвященная 56-летию Великой Победы",
                                          Date = DateTime.Parse("11.09.2001"),
                                          Curator = professor1,
                                          ConferenceType = ConferenceType.University,
                                          ConferenceReport = new ConferenceReport()
                                                             {
                                                                 OrganizationCorrectness = AccordanceLevel.Fully,
                                                                 ReportMaterials = AccordanceLevel.None,
                                                                 ThemeActuality = AccordanceLevel.Partly,
                                                                 ResultsUsage = AccordanceLevel.Partly
                                                             }
                                      },
                                      new Conference
                                      {
                                          Name = "Вторая международная конференция",
                                          Date = DateTime.Parse("13.08.2007"),
                                          Curator = professor2,
                                          ConferenceType = ConferenceType.University,
                                          ConferenceReport = new ConferenceReport()
                                                             {
                                                                 OrganizationCorrectness = AccordanceLevel.Fully,
                                                                 ReportMaterials = AccordanceLevel.None,
                                                                 ThemeActuality = AccordanceLevel.Partly,
                                                                 ResultsUsage = AccordanceLevel.Partly
                                                             }
                                      },
                                  };

                foreach (var conference in conferences)
                {
                    context.Conferences.Add(conference);
                }
            }

            private void SeedBooks(EntityContext context)
            {
                var professor1 = context.Set<Professor>()
                                        .AsQueryable()
                                        .Single(p => p.FullName.LastName == "Кашкаров");

                var professor2 = context.Set<Professor>()
                                        .AsQueryable()
                                        .Single(p => p.FullName.LastName == "Касанин");

                var books = new[]
                            {
                                new Book
                                {
                                    Name = "Учебник по специальной подготовке",
                                    PagesCount = 228,
                                    BookType = BookType.Schoolbook,
                                    Author = professor1,
                                },
                                new Book
                                {
                                    Name = "Учебник по тактической подготовке",
                                    PagesCount = 141,
                                    BookType = BookType.Schoolbook,
                                    Author = professor1,
                                },
                                new Book
                                {
                                    Name = "Как стать младшим офицером, " +
                                           "начальником радиостанций коротковолновых " +
                                           "малой мощности Р142-Н, Р145-БН за 21 день " +
                                           "или курс молодого связиста для чайников",
                                    PagesCount = 1309,
                                    BookType = BookType.Tutorial,
                                    Author = professor2,
                                },
                            };

                foreach (var book in books)
                {
                    context.Books.Add(book);
                }

                var t = context.Books.ToList();
            }

            #endregion // Class Private Methods
        }

        #region Class Properties

        public DbSet<Cathedra> Cathedras { get; set; }
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Book> Books { get; set; }

        #endregion // Class Properties

        #region Class Constructors

        public EntityContext(string connectionString)
            : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = true;
        }

        #endregion // Class Constructors

        #region Class Protected Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CathedraConfiguration());
            modelBuilder.Configurations.Add(new ProfessorConfiguration());
            modelBuilder.Configurations.Add(new ConferenceConfiguration());
            modelBuilder.Configurations.Add(new BookConfiguration());

            modelBuilder.Configurations.Add(new ConferenceReportConfiguration());
            modelBuilder.Configurations.Add(new FullNameConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        #endregion // Class Protected Methods
    }
}