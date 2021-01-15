using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LibraryManagement.Models
{
    public class ApplicationDbContextInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext> //CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            InitializeBizHolidays(context);
            InitializeBooks(context);
            InitializeCustomers(context);
            InitializeIdentities(context);
            base.Seed(context);
        }

        public static void InitializeIdentities(ApplicationDbContext db)
        {
            var roleStore = new RoleStore<IdentityRole>(db);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);

            // Add missing roles
            var librarianRole = roleManager.FindByName(Constants.LibrarianRoleName);
            if (librarianRole == null)
            {
                librarianRole = new IdentityRole(Constants.LibrarianRoleName);
                try { 
                roleManager.Create(librarianRole);
                }catch(Exception ex) {
                    Console.Write(ex); 
                }
            }

            var superVisorRole = roleManager.FindByName(Constants.SupervisorRoleName);
            if (superVisorRole == null)
            {
                superVisorRole = new IdentityRole(Constants.SupervisorRoleName);
                try
                {
                    roleManager.Create(superVisorRole);
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                
            }

            // add default users for test
            var librarianUser = userManager.FindByName(Constants.LibrarianRoleName);
            if (librarianUser == null)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = string.Format("{0}@veripark.test", Constants.LibrarianRoleName),
                    Email = string.Format("{0}@veripark.test", Constants.LibrarianRoleName),
                    EmailConfirmed = true,
                };

                userManager.Create(newUser, "Password1$");
                userManager.SetLockoutEnabled(newUser.Id, false);
                userManager.AddToRole(newUser.Id, Constants.LibrarianRoleName);
            }

            var supervisorUser = userManager.FindByName(Constants.SupervisorRoleName);
            if (supervisorUser == null)
            {
                var newUser = new ApplicationUser()
                {
                    UserName = string.Format("{0}@veripark.test", Constants.SupervisorRoleName),
                    Email = string.Format("{0}@veripark.test", Constants.SupervisorRoleName),
                    EmailConfirmed = true,
                };

                userManager.Create(newUser, "Password1$");
                userManager.SetLockoutEnabled(newUser.Id, false);
                userManager.AddToRole(newUser.Id, Constants.SupervisorRoleName);
            }
        }

        private void InitializeBizHolidays(ApplicationDbContext context)
        {
            List<BusinessHoliday> holidays = new List<BusinessHoliday>
            {
                new BusinessHoliday()
                {
                    HolidayOccassion = "Xmas",
                    FromDate = new DateTime(2020, 12, 25),
                    ToDate = new DateTime(2020, 12, 25)
                },
                new BusinessHoliday()
                {
                    HolidayOccassion = "NewYear",
                    FromDate = new DateTime(2021, 01, 01),
                    ToDate = new DateTime(2021, 01, 01)
                },
                new BusinessHoliday()
                {
                    HolidayOccassion = "Off Day",
                    FromDate = new DateTime(2021, 02, 06),
                    ToDate = new DateTime(2021, 02, 06)
                },
                new BusinessHoliday()
                {
                    HolidayOccassion = "Val Day",
                    FromDate = new DateTime(2021, 02, 12),
                    ToDate = new DateTime(2021, 02, 12)
                },
                new BusinessHoliday()
                {
                    HolidayOccassion = "Work day",
                    FromDate = new DateTime(2021, 05, 01),
                    ToDate = new DateTime(2021, 05, 01)
                },
            };
            context.BusinessHolidays.AddRange(holidays);
            #region 
            //BusinessHoliday holiday1 = new BusinessHoliday()
            //{
            //    HolidayOccassion = "Christmas",
            //    FromDate = new DateTime(2020, 12, 25),
            //    ToDate = new DateTime(2020, 12, 25)
            //};
            //BusinessHoliday holiday2 = new BusinessHoliday()
            //{
            //    HolidayOccassion = "New year",
            //    FromDate = new DateTime(2021, 01, 01),
            //    ToDate = new DateTime(2021, 01, 01)
            //};
            //BusinessHoliday holiday3 = new BusinessHoliday()
            //{
            //    HolidayOccassion = "Office holiday",
            //    FromDate = new DateTime(2021, 02, 06),
            //    ToDate = new DateTime(2021, 02, 06)
            //};
            //BusinessHoliday holiday4 = new BusinessHoliday()
            //{
            //    HolidayOccassion = "Valentine day",
            //    FromDate = new DateTime(2021, 02, 12),
            //    ToDate = new DateTime(2021, 02, 12)
            //};
            //BusinessHoliday holiday5 = new BusinessHoliday()
            //{
            //    HolidayOccassion = "Workers day",
            //    FromDate = new DateTime(2021, 05, 01),
            //    ToDate = new DateTime(2021, 05, 01)
            //};
            //context.BusinessHolidays.Add(holiday1);
            //context.BusinessHolidays.Add(holiday2);
            //context.BusinessHolidays.Add(holiday3);
            //context.BusinessHolidays.Add(holiday4);
            //context.BusinessHolidays.Add(holiday5);
            #endregion

        }

        private void InitializeBooks(ApplicationDbContext context)
        {
            Book book1 = new Book()
            {
                Title = "Corporate Chanakya (En)",
                SerialNumber = "9788184953480",
                Author = "Radhakrishnan Pillai",
                Publisher = "Jaico Publishing House"
            };

            Book book2 = new Book()
            {
                Title = "The Great Gatsby",
                SerialNumber = "9781983429156",
                Author = "F. Scott",
                Publisher = "Chatto & Windus"
            };

            Book book3 = new Book()
            {
                Title = "The Catcher in the Rye",
                SerialNumber = "9780316769310",
                Author = "J. D. Salinger",
                Publisher = "Little Brown & Co."
            };

            Book book4 = new Book()
            {
                Title = "The Alchemist",
                SerialNumber = "9788576653721",
                Author = "Paulo Coelho",
                Publisher = "HarperCollins"
            };

            Book book5 = new Book()
            {
                Title = "Mind Over Mood",
                SerialNumber = "9780616745687",
                Author = "Dennis Greenberger",
                Publisher = "Guilford"
            };

            Book book6 = new Book()
            {
                Title = "When Breath Becomes Air",
                SerialNumber = "9785040446797",
                Author = "Paul Kalanithi",
                Publisher = "Random House"
            };

            Book book7 = new Book()
            {
                Title = "A Suitable Boy",
                SerialNumber = "9780140230338",
                Author = "Vikram Seth",
                Publisher = "Orion Publishing Group"
            };

            Book book8 = new Book()
            {
                Title = "One night at the Call Center",
                SerialNumber = "9788184193916",
                Author = "Chetan Bhagat",
                Publisher = "Rupda & Co"
            };

            context.Books.Add(book1);
            context.Books.Add(book2);
            context.Books.Add(book3);
            context.Books.Add(book4);
            context.Books.Add(book5);
            context.Books.Add(book6);
            context.Books.Add(book7);
            context.Books.Add(book8);
        }

        private void InitializeCustomers(ApplicationDbContext context)
        {
            Customer cust1 = new Customer()
            {
                Name = "James Butt",
                Address = "6649 N Blue Gum St",
                Contact = "50-621 8927",
                NationalID = "12312312312"
            };

            Customer cust2 = new Customer()
            {
                Name = "Josephine Darakjy",
                Address = "Chanay, Jeffrey A Esq",
                Contact = "81-292 9840",
                NationalID = "123123124"
            };

            Customer cust3 = new Customer()
            {
                Name = "Art Venere",
                Address = "Chemel, James L Cpa",
                Contact = "85-636 8749",
                NationalID = "123123456"
            };


            Customer cust4 = new Customer()
            {
                Name = "Lenna Paprocki",
                Address = "Feltz Printing Service",
                Contact = "90-385 4412",
                NationalID = "32165498712"
            };

            context.Customers.Add(cust1);
            context.Customers.Add(cust2);
            context.Customers.Add(cust3);
            context.Customers.Add(cust4);

        }
    }
}

