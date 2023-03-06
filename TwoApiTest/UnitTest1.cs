using NUnit.Framework;
using RA;
using RestAssured;
using ClassLibrary1;
using ClassLibrary2;
using RestSharp;
using System;
using System.Text.RegularExpressions;
using ClassLibrary3;
using System.Linq;
using System.Collections.Generic;
using ClassLibrary5;
using Newtonsoft.Json;

namespace TwoApiTest
{
    [TestFixture]

    public class Tests : Specification
    {
        /*
         * Используя сервис http://reqres.in/, получить список пользователей со второй страницы;
         * Убедиться, что имена файлов-аватаров пользователей совпадают;
         * Убедиться, что email пользователей имеет окончание reqres.in;
         */

        [TestCase]
        public void Test1()
        {
            var library1 = new Class1();
            var response = GetMethod("/api/users?page=2");
            var users = library1.GetUsers(response);

            Assert.AreEqual(StatusCode200(), GetStatusCode(response), "Error statusCode");

            for (int i = 0; i < users.data.Count; i++)
            {
                // проверка, с использованием ренгулярного выражения
                Assert.AreEqual(Convert.ToInt32(Regex.Replace(users.data[i].avatar, @"[^\d]+", "")), users.data[i].id, "Error");
                // порверка на наличие id в email с омощью метода Contains
                Assert.IsTrue(users.data[i].avatar.Contains(users.data[i].id.ToString()));
            }

            for (int i = 0; i < users.data.Count; i++)
            {
                Assert.IsTrue(users.data[i].email.EndsWith("reqres.in"));
            }
        }

        /*
         * Используя сервис http://reqres.in/, протестировать регистрацию поьзователя в системе;
         * Необходимо создание 2 тестов:
         * - успешная регистрация;
         * - регистрация с ошиибкой из-за отсутствия пароля;
         * - проверить коды ошибок;
         */

        [TestCase]
        public void Test2()
        {
            var library2 = new Class2();
            Registration reg = new Registration("eve.holt@reqres.in", "pistol");
            string json = JsonConvert.SerializeObject(reg);
            var response = PostMethod("api/register", json);
            var resultRegistration = library2.SuccessfulResultRegistration(response);

            Assert.AreEqual(StatusCode200(), GetStatusCode(response), "Error statusCode");

            Assert.NotNull(resultRegistration.id, "NULL");
            Assert.NotNull(resultRegistration.token, "NULL");

            Assert.AreEqual(library2.id, resultRegistration.id, "Error registration");
            Assert.AreEqual(library2.token, resultRegistration.token, "Error registration");
        }

        [TestCase]
        public void Test3()
        {
            var library2 = new Class2();
            Registration reg = new Registration("sydney@fife", "");
            string json = JsonConvert.SerializeObject(reg);
            var response = PostMethod("api/register", json);
            var resultRegistration = library2.UnSuccessfulRegistration(response);

            Assert.AreEqual(StatusCode400(), GetStatusCode(response), "Error statusCode");
            Assert.AreEqual("Missing password", resultRegistration.Error, "Error");
        }

        ///*
        // * Используя сервис http://reqres.in/, убедиться, что операция LIST<RESOURCE> возвращает данные, отсортированные по годам;
        // */

        [TestCase]
        public void Test4()
        {
            var library3 = new Class3();
            var response = GetMethod("api/unknown");
            var years = library3.Years(response);
            var expectedSortDate = years.OrderBy(x => x);

            Assert.AreEqual(StatusCode200(), GetStatusCode(response), "Error statusCode");
            Assert.IsTrue(expectedSortDate.SequenceEqual(years), "The sort years is wrong");
        }

        ///*
        //* Используя сервис http://reqres.in/, попробовать удалить второго пользователя и сравнить статус-код;
        //*/

        [TestCase]
        public void Test5()
        {
            var response = DeleteMethod("/api/users/2");

            Assert.AreEqual(StatusCode204(), GetStatusCode(response), "Error statusCode");
        }

        ///*
        //* Используя сервис http://reqres.in/, обновить информацию о пользователе и сравнить дату обновления с текущей датой на машине;
        //*/

        [TestCase]
        public void Test6()
        {
            var library5 = new Class5();
            UpdateDate updateDate = new UpdateDate("morpheus", "zion resident");
            string json = JsonConvert.SerializeObject(updateDate);
            var response = PutMethod("/api/users/2", json);
            Assert.AreEqual(StatusCode200(), GetStatusCode(response), "Error statusCode");

            string dateUpdate = library5.Put(response);
            DateTimeOffset dateToday = DateTime.Today;
            string s = Regex.Replace(dateToday.ToString(), @"\s\d:\d{2}:\d{2}\s\+\d{2}:\d{2}", "");

            Assert.AreEqual(s, dateUpdate, "Error");
        }
    }
}