using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BankAccountApp;

namespace BankAccountTests
{
    [TestClass]
    public class BankAccountTests
    {
        [TestMethod]
        [DataRow("John Doe", 1000, 200, 800)]
        [DataRow("Jane Smith", 500, 100, 400)]
        [DataRow("Alice Johnson", 300, 50, 250)]
        public void Debit_ValidAmount_UpdatesBalance(string customerName, decimal
        initialBalance, decimal debitAmount, decimal expectedBalance)
        {
            // Arrange
            var account = new BankAccount(customerName, initialBalance);
            // Act
            account.Debit(debitAmount);
            // Assert
            Assert.AreEqual(expectedBalance, account.Balance);
        }
        [TestMethod]
        [DataRow("Bob Brown", 100, 150)]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Debit_InsufficientFunds_ThrowsException(string customerName,
        decimal initialBalance, decimal debitAmount)
        {
            // Arrange
            var account = new BankAccount(customerName, initialBalance);
            // Act
            account.Debit(debitAmount);
        }
        [TestMethod]
        [DataRow("John Doe", 1000, 200, 1200)]
        [DataRow("Jane Smith", 500, 100, 600)]
        [DataRow("Alice Johnson", 300, 50, 350)]
        public void Credit_ValidAmount_UpdatesBalance(string customerName, decimal
        initialBalance, decimal creditAmount, decimal expectedBalance)
        {
            // Arrange
            var account = new BankAccount(customerName, initialBalance);
            // Act
            account.Credit(creditAmount);
            // Assert
            Assert.AreEqual(expectedBalance, account.Balance);
        }
        [TestMethod]
        [DataRow("John Doe", 1000, 200, 800)]
        [DataRow("Jane Smith", 500, 100, 400)]
        [DataRow("Alice Johnson", 300, 50, 250)]
        public void Withdraw_ValidAmount_UpdatesBalance(string customerName, decimal
        initialBalance, decimal withdrawAmount, decimal expectedBalance)
        {
            // Arrange
            var account = new BankAccount(customerName, initialBalance);
            // Act
            account.Withdraw(withdrawAmount);
            // Assert
            Assert.AreEqual(expectedBalance, account.Balance);
        }
    }
}

