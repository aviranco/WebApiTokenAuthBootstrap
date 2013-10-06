Visit the documented Wiki for detailed examples and information.


## Features
**Token Based User Authentication**
User Property inside the TokenAuthApiController (Id, Username, Role, LastAccess).

**Token Based User Authorization**
TokenAuthorizeAttribute with Access Level - Public, User, Admin or Anonymous.

**Built-in Functionality**
Login(), Logoff(), Error(), Unauthorized() Responses with various overloads.

**Shared Dynamic Object Betwen Client and Server**
UserData Property inside the TokenAuthApiController (Up to size of 4 KB storage).
Great for caching data in the client side, especially for thin back-end applications like Single Page Applications.

## Getting Started
Install Package from Nuget console:
``` Install-Package WebApiTokenAuth ```

Now, your controllers should inherit from `TokenAuthApiController` instead of the default ApiController,
so you can access its extensions:
* Access to the properties `UserMetadata User` and `dynamic UserData`.
* Access following functions: `Login()`, `Logout()`, `Error()` and `Unauthorize()`.

**Note**

_In order to use the `UserData` dynamic object that enables client side caching using cookies,_
_add the following line to the `Application_Start()` function inside the `Global.asax` file:_
_`GlobalConfiguration.Configuration.Filters.Add(new UserDataModificationActionFilter());`_

### Code Sample
Here I demonstrate the simplicity of using the **WebApiTokenAuth** package with Register,
Login and Logout functionality.

    /// <summary>
    /// Handles all the account related actions - user registration, login and logout.
    /// </summary>
    public class AuthController : TokenAuthApiController
    {
        // GET api/auth/login
        [ActionName("login")]
        [TokenAuthentication(AccessLevel.Anonymous)]
        public HttpResponseMessage PostLogin([FromBody]LoginViewModel user)
        {
            // Input validaiton.
            if (user == null || user.Username == null || user.Password == null)
            {
                return Error("Please enter username and password.");
            }

            // Retrieve the user data from the Data access layer.
            IDal dal = new ProGamersDal();
            var currentUser = dal.GetUser(user.Username, user.Password);

            // If not match found - return error.
            if (currentUser == null)
            {
                return Error("Bad username or password.");
            }

            // Cache username and user role at the client side as cookie - accessible by javascript at the client side as json object.
            // Note this data is not secured since the user can access the cookie. Don't store any sensitive information there.
            // In case you save login data in the client side as I did, Server-side validation is a MUST.
            UserData.username = "Dgandalf";
            UserData.role = (int) UserRole.User;

            // Creates an access token for this user, stores it in the configured TokenStorage (By default use in-memory storage).
            // You can set different TokenStorage at TokenAuthenticationConfiguration.TokenStorage in your Application_Start 
            // function inside theg lobal.asax file. Additionally, sends cookie with the generated access token to the user.
            return Login(1, "Dgandalf", UserRole.User);

        }

        // POST api/auth/logout
        [ActionName("logout")]
        [TokenAuthentication(AccessLevel.User)]
        public HttpResponseMessage PostLogout()
        {
            // Deletes the token and user-data cookies with the generated access token to the user.
            return Logout();
        }

        // POST api/auth/register
        [ActionName("register")]
        [TokenAuthentication(AccessLevel.Anonymous)]
        public HttpResponseMessage PostRegister(User user)
        {
            // Handle registration data here.

            // Returns OK response. You can also use Login() function instead, so the user will be logged in 
            // automaticly after a successful registration.
            return Ok();
        }
    }

### Support or Contact
Having trouble or any questions related to WebAPI Token Auth Bootstrap? Feel free to contact me at:
admin@AviranCohen.com
