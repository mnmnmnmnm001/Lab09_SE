using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using BankAccountApp;

namespace BankAccountTests
{
    [TestClass]
    public class BankAccountCsvTests
    {
        public static IEnumerable<object[]> GetTestData()
        {
            using (var reader = new StreamReader("BankAccountTestData.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                foreach (var record in csv.GetRecords<TestData>())
                {
                    yield return new object[] { record.CustomerName, record.InitialBalance, record.DebitAmount, record.ExpectedBalance };
                }
            }
        }
        [TestMethod]
        public void Debit_CsvData_UpdatesBalance()
        {
            foreach (var testData in GetTestData())
            {
                var customerName = (string)testData[0];
                var initialBalance = (decimal)testData[1];
                var debitAmount = (decimal)testData[2];
                var expectedBalance = (string)testData[3];

                // Arrange
                var account = new BankAccount(customerName, initialBalance);

                // Act & Assert
                if (expectedBalance == "Insufficient funds")
                {
                    try
                    {
                        account.Debit(debitAmount);
                        Assert.Fail("Expected InvalidOperationException");
                    }
                    catch (InvalidOperationException)
                    {
                        // Expected behavior
                    }
                }
                else
                {
                    account.Debit(debitAmount);
                    Assert.AreEqual(decimal.Parse(expectedBalance), account.Balance);
                }
            }
        }
        public class TestData
        {
            public string CustomerName { get; set; }
            public decimal InitialBalance { get; set; }
            public decimal DebitAmount { get; set; }
            public string ExpectedBalance { get; set; }
        }
    }
}