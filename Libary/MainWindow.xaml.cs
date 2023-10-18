using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Lidary.Model;


namespace Libary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<User> users = new List<User>()
            {
                new User(1, "векс", "лиговна"),
                new User(2, "пудж", "дотович"),
                new User(3, "найкс", "дотович"),
                new User(4, "висп", "айкюов"),
                new User(5, "кэталина", "лиговская"),
                new User(6, "Ле Блан", "лиголегендская")
                
            };

        List<Book> books = new List<Book>()
            {
                new Book(1, "философия жизни","Жмышенко Валерий Альбертович", 2020, 1, 27, 5),
                new Book(2, "загадки", "Жак Фреско",  2023, 3, 10, 10),
                new Book(3, "капитализм?", "Яна Цист", 2022, 2, 1, 1),
                new Book(4, "детские сказки", "Гена Цидормян", 2018, 1, 25, 2)
            };


        public MainWindow()
        {
            InitializeComponent();

            UserList.ItemsSource = users;

            BookList.ItemsSource = books;

            SelectBook.ItemsSource = books;
            SelectUser.ItemsSource = users;

        }
        private void AddSelectedBook(object sender, RoutedEventArgs e)
        {
            AddBookUser();
        }

        private void AddBookUser()
        {
            User selectedUser = (User)SelectUser.SelectedItem;
            Book selectedBook = (Book)SelectBook.SelectedItem;
            if (selectedUser != null && selectedBook != null)
            {
                selectedUser.BorrowBook(selectedBook, selectedUser);
                UpdateJournal();
                BookList.Items.Refresh();
                Mini_Journal.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Выберите пользователя и книгу");
            }
        }
        
        private void UpdateJournal()
        {
            List<Book> allBooks = new List<Book>();
            foreach (var user in (UserList.ItemsSource as List<User>)!)
            {
                allBooks.AddRange(user.Books);
            }
            Journal.ItemsSource = allBooks;
        }


        private void AddUser(object sender, RoutedEventArgs e)
        {
            if (TextName.Text != "" || TextFamily.Text != "")
            { 
                User value = new User(users[^1].Id, TextName.Text, TextFamily.Text);
                value.Id++;
                users.Add(value);
                UserList.Items.Refresh();
                SelectUser.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Заполните поля");
            }
        }

        private void AddBook(object sender, RoutedEventArgs e)
        {
            try
            {

                int Year = Convert.ToInt32(TextYear.Text);
                int Mounth = Convert.ToInt32(TextMounth.Text);
                int Day = Convert.ToInt32(TextDay.Text);
                int Count = Convert.ToInt32(TextCount.Text);
                Book value =
                    new Book(books[^1].Act,
                    TextBookName.Text,
                    TextAuthor.Text,
                    Year,
                    Mounth,
                    Day,
                    Count);

                value.Act++;
                books.Add(value);
                BookList.Items.Refresh();
                SelectBook.Items.Refresh();
            }
            catch
            {
                MessageBox.Show("Введите в поля года, месяца, дней и колличество книг числа");
            }
        }


        private void Serch_in_Users(object sender, TextChangedEventArgs e)
        {
            if (SerchBox.Text == "")
            {
                UserList.ItemsSource = users;
                UserList.Items.Refresh();
            }
            else
            {
                 var SerchUser = from u in users
                                 where u.Name.ToLower().StartsWith(SerchBox.Text) || u.Name.ToUpper().StartsWith(SerchBox.Text)
                                 orderby u
                                 select u;

                UserList.ItemsSource = SerchUser;
                UserList.Items.Refresh();
            }
        }

        private void SelectUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User? value = SelectUser.SelectedItem as User;

            Mini_Journal.ItemsSource = value.Books;

            Mini_Journal.Items.Refresh();
        }

        private void SerchUser(object sender, TextChangedEventArgs e)
        {
            if (SerchUsers.Text == "")
            {
                SelectUser.ItemsSource = users;
                SelectUser.Items.Refresh();
            }
            else
            {
                var SerchUser = from u in users
                                where u.Name.ToLower().StartsWith(SerchUsers.Text) || u.Name.ToUpper().StartsWith(SerchUsers.Text)
                                orderby u
                                select u;

                SelectUser.ItemsSource = SerchUser;
                SelectUser.Items.Refresh();
            }
        }

        private void SerchBook(object sender, TextChangedEventArgs e)
        {
            if (SerchBooks.Text == "")
            {
                SelectBook.ItemsSource = books;
                SelectBook.Items.Refresh();
            }
            else
            {
                var SerchBook = from u in books
                                    where u.Name.ToLower().Contains(SerchBooks.Text) || u.Name.ToUpper().Contains(SerchBooks.Text)
                                    orderby u
                                    select u;
                SelectBook.ItemsSource = SerchBook;
                SelectBook.Items.Refresh();
            }
        }
    }
}
