using System;
using HanksMineralEmporium.Core.UserManagement;
using HanksMineralEmporium.Core.UserManagement.Exception;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;


namespace HanksMineralEmporium.Service.AuthenticationService
{
    
    public class  AuthenticationService : IAuthenticationService
    {
        private readonly IUserManager _userManager;
        
    async void LoginUser(string username, string password)
{
    try
    {
         var serviceProvider = new ServiceCollection()
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddDefaultTokenProviders()
                .Services
                .BuildServiceProvider();
        // Create a UserManager
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var signInManager = serviceProvider.GetRequiredService<SignInManager<IdentityUser>>();

            bool rememberMe = false;

        // Find the user by username
            var user = await userManager.FindByNameAsync(username);
            

        if (user != null) 
        {
            Console.Write("User found");
             // Attempt to sign in the user
             // READ not sure if password validation is handled by the result or not
                var result = await signInManager.PasswordSignInAsync(user, password, rememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    Console.WriteLine("User logged in successfully.");
                }
                 else
                {
                    Console.WriteLine("Invalid username or password.");
                }
                
        }

        else{throw new Core.UserManagement.Exception.UserNotFoundException("User not found");}        
             
    }
    catch (Core.UserManagement.Exception.UserNotFoundException ex)
    {
        Console.WriteLine($"UserNotFoundException: {ex.Message}");

    }
    
}
async void RegisterUser(string username, string password){
         var serviceProvider = new ServiceCollection()
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddDefaultTokenProviders()
                .Services
                .BuildServiceProvider();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

         var newUser = new IdentityUser
            {
                UserName = username,
                PasswordHash = password
            };
            if(newUser.UserName.Length < 3){
                throw new Core.UserManagement.Exception.InvalidUsernameException("Username too short");
            }
            else if(newUser.UserName != null){
                throw new Core.UserManagement.Exception.InvalidUsernameException("Username already taken");
            }
            
            if(newUser.PasswordHash.Length < 8){
                throw new Core.UserManagement.Exception.InvalidPasswordException("Password too short");
            }

         // Create the user
            var result = await userManager.CreateAsync(newUser);
            if (result.Succeeded)
            {
                Console.WriteLine("User created successfully.");
            }
            

}
        public AuthenticationService(IUserManager userManager)
    {
        // Ensure that the IUserManager object is not null
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    }
}