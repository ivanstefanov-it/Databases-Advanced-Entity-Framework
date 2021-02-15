using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BillsPaymentSystem.Data;
using BillsPaymentSystem.Models;
using BillsPaymentSystem.Models.Enums;

namespace BillsPaymentSystem.App
{
    public class DbInitializer
    {
        public static void Seed(BillsPaymentSystemContext context)
        {
           SeedUser(context);
           SeedCredirCard(context);
           SeedBankAccounts(context);
            SeedPaymentMethods(context);
        }

        private static void SeedPaymentMethods(BillsPaymentSystemContext context)
        {
            List<PaymentMethod> paymentMethods = new List<PaymentMethod>();
            for (int i = 0; i < 3; i++)
            {
                var paymentMethod = new PaymentMethod
                {
                    UserId = new Random().Next(1, 7),
                    Type = (PaymentType)new Random().Next(0, 2)
                };

                if (i % 3 == 0)
                {
                    paymentMethod.CreditCardId = new Random().Next(1, 5);
                    paymentMethod.BankAccountId = new Random().Next(1, 5);
                }
                else if (i % 2 == 0)
                {
                    paymentMethod.CreditCardId = new Random().Next(1, 5);
                }
                else
                {
                    paymentMethod.BankAccountId = new Random().Next(1, 5);
                }

               
                if (IsValid(paymentMethod) == false)
                {
                    continue;
                }
                
                paymentMethods.Add(paymentMethod);
            }

            context.PaymentMethods.AddRange(paymentMethods);
            context.SaveChanges();
        }

        private static void SeedBankAccounts(BillsPaymentSystemContext context)
        {
            List<BankAccount> bankAccounts = new List<BankAccount>();
            for (int i = 0; i < 8; i++)
            {
                var bankAccount = new BankAccount
                {
                    Balance = new Random().Next(-500, 1000),
                    BankName = "Bank" + i,
                    SWIFTCode = "Swift" + i + 1
                };

                if (IsValid(bankAccount) == false)
                {
                    continue;
                }

                bankAccounts.Add(bankAccount);
            }

            context.BankAccounts.AddRange(bankAccounts);
            context.SaveChanges();
        }

        private static void SeedCredirCard(BillsPaymentSystemContext context)
        {
            List<CreditCard> creditCards = new List<CreditCard>();

            for (int i = 0; i < 8; i++)
            {
                var creditCard = new CreditCard
                {
                    Limit = new Random().Next(-250000, 250000),
                    MoneyOwed = new Random().Next(-250000, 250000),
                    ExpirationDate = DateTime.Now.AddDays(new Random().Next(-200, 200))
                };

                if (IsValid(creditCard) == false)
                {
                    continue;
                }

                creditCards.Add(creditCard);
            }

            context.CreditCards.AddRange(creditCards);
            context.SaveChanges();
        }

        private static void SeedUser(BillsPaymentSystemContext context)
        {
            string[] firstNames =
            {
                "Ivan",
                "Maria",
                "Alex",
                "Mila",
                "Vasil",
                "Petko"
            };

            string[] lastNames =
            {
                "Stafanov",
                "Pavlova",
                "Bahareva",
                "Spasova",
                "Draganov",
                "Tashev"
            };

            string[] emails =
            {
                "Ivan@gmail.com",
                "Maria@gmail.com",
                "Alex@gmail.com",
                "Mila@gmail.com",
                "Vasil@gmail.com",
                "Petko@gmail.com"
            };

            string[] passwords =
            {
                "123456",
                "1234567",
                "1234568",
                "1234569",
                "1234561",
                "1234562"
            };

            List<User> users = new List<User>();

            for (int i = 0; i < firstNames.Length; i++)
            {
                var user = new User
                {
                    FirstName = firstNames[i],
                    LastName = lastNames[i],
                    Email = emails[i],
                    Password = passwords[i],

                };
                if (IsValid(user) == false)
                {
                    continue;
                }

                users.Add(user);
            }

            context.Users.AddRange(users);
            context.SaveChanges();    
        }

        private static bool IsValid(object entity)
        {
            var validationContext = new ValidationContext(entity);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entity, validationContext, validationResults, true);

            return isValid;
        }
    }
}
