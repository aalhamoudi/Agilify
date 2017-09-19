using System;
using System.Collections.Generic;
using System.Text;
using Agilify.Models;
using Xamarin.Forms;

namespace Agilify.Views.ApplicationPages
{
    class RegistrationPage : ContentPage
    {
        public UserBindingModel User { get; set; }
        public RegistrationPage()
        {
            Title = "Register";
            BindingContext = User = new UserBindingModel();

            var emailField = new Entry
            {
                Placeholder = "Email"
            };

            var nameField = new Entry
            {
                Placeholder = "Name"
            };

            var passwordField = new Entry
            {
                Placeholder = "Password",
                IsPassword = true
            };

            var passwordConfirmationField = new Entry
            {
                Placeholder = "Password Confirmation",
                IsPassword = true
            };

            var registerButton = new Button
            {
                Text = "Register"
            };

            emailField.SetBinding(Entry.TextProperty, "Email");
            nameField.SetBinding(Entry.TextProperty, "Name");
            passwordField.SetBinding(Entry.TextProperty, "Password");
            passwordConfirmationField.SetBinding(Entry.TextProperty, "ConfirmPassword");

            registerButton.Clicked += OnRegister;

            Content = new StackLayout
            {
                Padding = new Thickness(10),
                Children =
                {
                    new StackLayout
                    {
                       Children =
                       {
                           emailField,
                           nameField,
                           passwordField,
                           passwordConfirmationField,
                           registerButton
                       }
                    }
                }
            };
        }

        private async void OnRegister(object sender, EventArgs e)
        {
            try
            {
                var res = await App.CloudClient.InvokeApiAsync<UserBindingModel, Member>("account/create", User);
                if (res == null)
                    await DisplayAlert("Registeration", "Registration failed", "OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await DisplayAlert("Registeration", ex.Message, "OK");

            }


        }
    }
}
