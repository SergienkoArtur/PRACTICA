using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using static WpfApp3.MainWindow;

namespace WpfApp3
{
    public class user
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }

    public class AppDbContext : DbContext
    {
        public DbSet<transport> transports { get; set; }

        public DbSet<user> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = UsersDb; Integrated Security = True; Connect Timeout = 30; Encrypt = False; Trust Server Certificate = False; Application Intent = ReadWrite; Multi Subnet Failover = False");
        }
    }


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void vhod_Click(object sender, RoutedEventArgs e)
        {
            var login = log.Text;
            var password = passw.Text;

            var context = new AppDbContext();

            var user_exists = context.Users.SingleOrDefault(x => x.Login == login && x.Password == password);

            if(user_exists is not null)
            {
                MessageBox.Show("Это регистрация а не авторизация критин");
                return;
            }

            var user = new user { Login = login, Password = password };

            context.Users.Add(user);
            context.SaveChanges();
        }

        private void avt_Click(object sender, RoutedEventArgs e)
        {
            var login = avt_log.Text;
            var password = avt_passw.Text;

            var context = new AppDbContext();

            var user = context.Users.SingleOrDefault(x => x.Login == login && x.Password == password);
            if(user is null)
            {
                MessageBox.Show("Неправильный логин или пароль, i think что тебе нужно пробовать еще");
                return;
            }

            MessageBox.Show("Вы успешно вошли в аккаунт");
        }

        public class transport
        {
            public int ID { get; set; }

            public user Owner { get; set; }

            public string Identifier { get; set; }
        }
}
}
