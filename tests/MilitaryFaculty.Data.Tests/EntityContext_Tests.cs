using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using MilitaryFaculty.Domain;
using NUnit.Framework;

// ReSharper disable InconsistentNaming

namespace MilitaryFaculty.Data.Tests
{
    [TestFixture]
    public class EntityContext_Tests
    {
        [SetUp]
        public void SetUp()
        {
            const string conName = "Current";

            var connectionString = ConfigurationManager.ConnectionStrings[conName]
                .ConnectionString;

            Database.SetInitializer(new DropCreateDatabaseAlways<EntityContext>());
            context = new EntityContext(connectionString);
        }

        private EntityContext context;

        private static void SeedCathedras(EntityContext context)
        {
            var cathedras = new[]
            {
                new Cathedra {Name = "Кафедра тактики"},
                new Cathedra {Name = "Кафедра связи"},
                new Cathedra {Name = "Кафедра ПВО"}
            };

            foreach (var cathedra in cathedras)
            {
                context.Cathedras.Add(cathedra);
            }

            context.SaveChanges();
        }


        private static void SeedProfessors(EntityContext context)
        {
            var cathedra = context.Cathedras
                                  .AsQueryable()
                                  .Single(c => c.Name == "Кафедра связи");

            var professors = new[]
            {
                new Person
                {
                    FullName = new FullName("Сергей", "Николаевич", "Касанин"),
                    Cathedra = cathedra,
                    MilitaryRank = MilitaryRank.Colonel,
                    JobPosition = JobPosition.Professor,
                    EnrollmentDate = DateTime.Now
                },
                new Person
                {
                    FullName = new FullName("Андрей", "Васильевич", "Кашкаров"),
                    Cathedra = cathedra,
                    MilitaryRank = MilitaryRank.Colonel,
                    JobPosition = JobPosition.Professor,
                    EnrollmentDate = DateTime.Now
                },
                new Person
                {
                    FullName = new FullName("Роман", "Анатольевич", "Градусов"),
                    Cathedra = cathedra,
                    MilitaryRank = MilitaryRank.Colonel,
                    JobPosition = JobPosition.Professor,
                    EnrollmentDate = DateTime.Now
                },
                new Person
                {
                    FullName = new FullName("Геннадий", "Юрьевич", "Дюжов"),
                    Cathedra = cathedra,
                    MilitaryRank = MilitaryRank.Colonel,
                    JobPosition = JobPosition.Professor,
                    EnrollmentDate = DateTime.Now
                }
            };

            foreach (var professor in professors)
            {
                context.Professors.Add(professor);
            }

            context.SaveChanges();
        }

        private static void SeedConferences(EntityContext context)
        {
            var professor1 = context.Professors
                                    .AsQueryable()
                                    .Single(p => p.FullName.LastName == "Кашкаров");

            var professor2 = context.Set<Person>()
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
                    EventLevel = EventLevel.University,
                    ConferenceReport = new ConferenceReport
                    {
                        OrganizationCorrectness = ConferenceAccordance.Fully,
                        ReportMaterials = ConferenceAccordance.None,
                        ThemeActuality = ConferenceAccordance.Partly,
                        ResultsUsage = ConferenceAccordance.Partly
                    }
                },
                new Conference
                {
                    Name = "Военно-научная конференция, посвященная 56-летию Великой Победы",
                    Date = DateTime.Parse("11.09.2001"),
                    Curator = professor1,
                    EventLevel = EventLevel.University,
                    ConferenceReport = new ConferenceReport
                    {
                        OrganizationCorrectness = ConferenceAccordance.Fully,
                        ReportMaterials = ConferenceAccordance.None,
                        ThemeActuality = ConferenceAccordance.Partly,
                        ResultsUsage = ConferenceAccordance.Partly
                    }
                },
                new Conference
                {
                    Name = "Вторая международная конференция",
                    Date = DateTime.Parse("13.08.2007"),
                    Curator = professor2,
                    EventLevel = EventLevel.University,
                    ConferenceReport = new ConferenceReport
                    {
                        OrganizationCorrectness = ConferenceAccordance.Fully,
                        ReportMaterials = ConferenceAccordance.None,
                        ThemeActuality = ConferenceAccordance.Partly,
                        ResultsUsage = ConferenceAccordance.Partly
                    }
                }
            };

            foreach (var conference in conferences)
            {
                context.Conferences.Add(conference);
            }

            context.SaveChanges();
        }

        private static void SeedPublications(EntityContext context)
        {
            var professor1 = context.Professors
                                    .AsQueryable()
                                    .Single(p => p.FullName.LastName == "Кашкаров");

            var professor2 = context.Professors
                                    .AsQueryable()
                                    .Single(p => p.FullName.LastName == "Дюжов");

            var publications = new[]
            {
                new Publication
                {
                    Name = "Учебник по специальной подготовке",
                    PagesCount = 228,
                    PublicationType = PublicationType.Article,
                    Author = professor1,
                    CreatedAt = DateTime.Now
                },
                new Publication
                {
                    Name = "Учебник по тактической подготовке",
                    PagesCount = 141,
                    PublicationType = PublicationType.Article,
                    Author = professor1,
                    CreatedAt = DateTime.Now
                },
                new Publication
                {
                    Name = "Как стать младшим офицером, " +
                           "начальником радиостанций коротковолновых " +
                           "малой мощности Р142-Н, Р145-БН за 21 день " +
                           "или курс молодого связиста для чайников",
                    PagesCount = 1309,
                    PublicationType = PublicationType.Article,
                    Author = professor2,
                    CreatedAt = DateTime.Now
                }
            };


            foreach (var publication in publications)
            {
                context.Publications.Add(publication);
            }

            context.SaveChanges();
        }

        private static void SeedBooks(EntityContext context)
        {
            var professor1 = context.Professors
                                    .AsQueryable()
                                    .Single(p => p.FullName.LastName == "Кашкаров");

            var books = new[]
            {
                new Book
                {
                    Name = "Воинский устав в картинках. Для детей дошкольного возраста.",
                    PagesCount = 28,
                    Author = professor1,
                    BookType = BookType.Tutorial,
                    CreatedAt = DateTime.Now
                },
                new Book
                {
                    Name = "Моя служба. Мемуары.",
                    PagesCount = 1411,
                    BookType = BookType.Schoolbook,
                    Author = professor1,
                    CreatedAt = DateTime.Now
                }
            };


            foreach (var book in books)
            {
                context.Books.Add(book);
            }

            context.SaveChanges();
        }

        private static void SeedExhibitions(EntityContext context)
        {
            var professor1 = context.Professors
                                    .AsQueryable()
                                    .Single(p => p.FullName.LastName == "Кашкаров");

            var exhibitions = new[]
            {
                new Exhibition
                {
                    Name = "Военная научная выставка.",
                    Participant = professor1,
                    Award = ExhibitionAward.ThirdDegree,
                    Date = DateTime.Today
                },
                new Exhibition
                {
                    Name = "Тибо 2001.",
                    Award = ExhibitionAward.FirstDegree,
                    Participant = professor1,
                    Date = DateTime.Today
                }
            };


            foreach (var exhibition in exhibitions)
            {
                context.Exhibitions.Add(exhibition);
            }

            context.SaveChanges();
        }


        private static void SeedScienceRankMetricDefinitions(EntityContext context)
        {
            var definitions =
                new Dictionary<string, int>
                {
                    {"Полнота разработки планирующих документов", 0},
                    {"Уровень соответствия тематики исследований", 0},
                    {"Уровень ознакомления лиц из числа ППС с требованиями правовых актов", 0},
                    {"Уровень организации международных конференций", 0},
                    {"Уровень организации республиканских конференций", 0},
                    {"Уровень организации вузовских конференций", 0},
                    {"Уровень организации республиканских семинаров", 0},
                    {"Уровень организации вузовских семинаров", 0},
                    {
                        "Уровень организации подготовки научно-педагогических работников " +
                        "высшей квалификации",
                        0
                    },
                    {"Уровень организации военно-исторической работы", 0},
                    {"Уровень организации работы научных кружков", 0},
                    {
                        "Другие частные показатели, характеризующие качество " +
                        "организации научной работы",
                        0
                    },
                    {
                        "Другие частные показатели, характеризующие проведение " +
                        "научных исследований",
                        0
                    },
                    {
                        "Другие частные показатели, характеризующие апробацию результатов" +
                        "научных исследований",
                        0
                    },
                    {
                        "Другие частные показатели, характеризующие подготовку " +
                        "и аттестацию работников высшей квалификации",
                        0
                    }
                };

            foreach (var pair in definitions)
            {
                context.ScientificRankMetricDefinitions.Add(new ScienceRankMetricDefinition(pair.Key, pair.Value));
            }

            context.SaveChanges();
        }


        [Test]
        public void InitDatabase()
        {
            SeedCathedras(context);
            SeedProfessors(context);
            SeedConferences(context);
            SeedPublications(context);
            SeedBooks(context);
            SeedExhibitions(context);
            SeedScienceRankMetricDefinitions(context);
        }
    }
}

// ReSharper restore InconsistentNaming