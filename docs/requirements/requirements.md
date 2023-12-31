# Requirements
- [Version 1](#version-1)
- [Version 2](#version-2)

## Version 1
- Milestone: Requirements
  - T1E-1: Users
    - T1S-1.1: Register General Users
      - Must have
      - 3 days effort
      - Non-Functional
      - The system allows users to register in the database using a unique username and a 6-character minimum password. Users can add items to their cart and checkout items.
    - T1S-1.2: Create Admin Users
      - Must have
      - 3 days effort
      - Non-Functional
      - The system allows other admin users to transform a normal user into an admin user. Admins have the same abilities as general users but can also run sales reports and add new items to the Product List (T1E-2).
    - T1S-1.3: Seed User Database
      - Must have
      - 1 day effort
      - Non-Functional
      - The system populates the user database with a default admin account so that new admins can be created.
  - T1E-2: Product List
    - T1S-2.1: Display Item Information
      - Must have
      - 3 days effort
      - Non-Functional
      - The system serves a name, picture, price, and description for each item in the database. Items that have been sold should not be served on the Product List Page (T1E-7).
    - T1S-2.2: Add Item to Cart
      - Must have
      - 3 days effort
      - Functional
      - The system provides a way for a user to add an item on the Product List Page (T1E-7) to their cart. A user must be logged into an account to do this.
    - T1S-2.3: Search Item by Name
      - Must have
      - 4 days effort
      - Functional
      - The system provides a way to query available products by name.
  - T1E-3: Item Checkout
    - T1S-3.1: Checkout Item List
      - Must have
      - 2 days effort
      - Functional
      - The system provides a list of items in a user's shopping cart. The cart items are removable from the cart. A subtotal of the items in the cart needs to be provided.
    - T1S-3.2: Accept Payment Info
      - Must have
      - 3 days effort
      - Functional
      - The system requires a shipping address, phone number, credit card number, credit card expiration date, and credit card CVV number.
    - T1S-3.3: Provide Shipping Option
      - Must have
      - 2 days effort
      - Functional
      - The system requires the user to select a shipping option. The options are free ground shipping, $19 3-day shipping, and $29 overnight shipping.
    - T1S-3.4: Provide Order Summary
      - Must have
      - 1 day effort
      - Functional
      - The system provides a list of the names of each ordered item and their price, a subtotal, the calculated tax (6%), the shipping price, and a grand total.
    - T1S-3.5: Provide Receipt
      - Must have
      - 3 days effort
      - Functional
      - The system will provide a receipt when an order is completed. The receipt must have all information from the Order Confirmation (T1S-3.4) in addition to the last 4 digits of the provided credit card number and the shipping address.
    - T1S-3.6: Confirm Sale
      - Must have
      - 3 days effort
      - Non-Functional
      - The system will save sales in the database. Products that have been sold must no longer be served on the Product List Page (T1E-7).
  - T1E-4: Sales Report
    - T1S-4.1: Generate Sales Report
      - Must have
      - 5 days effort
      - Functional
      - The system provides a list of all sales in the database. Each sale includes what was sold, who purchased it, and the Receipt (T1S-3.5) from that purchase. This function can only be run by admins.
    - T1S-4.2: Export Sales Report
      - Needs to have
      - 2 days effort
      - Functional
      - The system dumps the sales report into a CSV file and supplies it to the admin to download.
  - T1E-5: Entering New Product Items
    - T1S-5.1: Seed Item Database
      - Needs to have
      - 1 day effort
      - Non-Functional
      - The system populates the product item database so that the Product List Page (T1E-7) is not empty.
    - T1S-5.2: Create New Product Item
      - Must have
      - 2 days effort
      - Functional
      - The system allows an admin to enter a name, select a picture, enter a price, and enter a description for a new item.
    - T1S-5.3: Append New Product Item
      - Must have
      - 3 days effort
      - Non-Functional
      - The system appends a newly created item to the items database.
- Milestone: UI Design
  - T1E-6: Login/Registration
    - T1S-6.1: Register User Page
      - Must have
      - 4 days effort
      - Functional
      - The system displays a page where a user can input a unique username and password. If either the username or password is invalid, the account will not be created and the user will be asked to try again.
    - T1S-6.2: Account Login Page
      - Must have
      - 4 days effort
      - Functional
      - The system displays a page where a user can log in to their account by inputting a username and password. If the user does not have an account, they can press a button to be brought to the Register User Page (T1S-6.1).
  - T1E-7: Product List Page
    - T1S-7.1: Display Item Display
      - Must have
      - 2 days effort
      - Functional
      - The page shows displayed products with a name, picture, price, and description. Sold items are not shown.
    - T1S-7.2: Add Item to Shopping Cart Button
      - Must have
      - 3 days effort
      - Functional
      - The page provides a button for a user to add a displayed item to their cart.
    - T1S-7.3: Search Item by Name
      - Must have
      - 4 days effort
      - Functional
      - The page offers a search bar where a user can enter a search key and items with similar names to the key will be shown.
  - T1E-8: Shopping Cart & Checkout Page
    - T1S-8.1: Access Checkout Page
      - Must have
      - 3 days effort
      - Functional
      - The system enables a user to access checkout by clicking a button once they have added at least one item to the cart. The system lists the items in the cart with a calculated subtotal and allows them to be removed with a button press. The system gives the user a button to proceed to the Pay Now page.
    - T1S-8.2: Fill out Payment Info
      - Must have
      - 2 days effort
      - Functional
      - The page requires the user to fill out a valid shipping address, phone number, credit card number, credit card expiration date, and credit card CVV number before proceeding.
    - T1S-8.3: Select Shipping Option
      - Must have
      - 1 days effort
      - Functional
      - The page requires a user to select a Provided Shipping Option (T1S-3.3) before proceeding.
    - T1S-8.4: View Order Summary
      - Must have
      - 2 days effort
      - Functional
      - The page shows a list of all item names and their prices, a subtotal, the calculated tax owed, the shipping cost, and a grand total.
    - T1S-8.5: Complete Order
      - Must have
      - 1 day effort
      - Functional
      - The user can press a complete order button once all fields have been filled out.
    - T1S-8.6: View Receipt Page
      - Must have
      - 2 days effort
      - Functional
      - The page shows all of the information from the order summary, the last 4 digits of the credit card number, and the shipping address. The user can press an OK button to return to the Product List Page (T1E-7).
  - T1E-9: Sales Report Page
    - T1S-9.1: View Sales Report
      - Must have
      - 4 days effort
      - Functional
      - The page displays a list of all sales to the admin. Each sale includes the product name and who bought it. The page allows the admin to click an individual sale to show that sale's Receipt Page (T1S-8.6).
    - T1S-9.2: Export Sales Report
      - Must have
      - 2 days effort
      - Functional
      - The page provides a button that allows an admin to download the generated sales report.
  - T1E-10: Item Creation Page
    - T1S-10.1: Enter Item Information
      - Needs to have
      - 1 day effort
      - Functional
      - The page requires the admin to fill out an item name, item price, and item description field. The page also offers a list of available images for the admin to select from.
    - T1S-10.2: Select Item Image
      - Wants to have
      - 3 days effort
      - Functional
      - The page shows a box with all available images. The page enables the admin to scroll down through the images and click the desired image to select it. When an image is selected, the window displaying the images is closed and the selected image is shown. A new image can be selected if necessary.
    - T1S-10.3: Submit Item to Database
      - Must have
      - 4 days effort
      - Functional
      - The page allows an admin to submit the item to the database once all fields have been filled out.
- Milestone: Technical Design
  - T1E-11: Database Components
    - T1S-11.1: Store Users
      - Must have
      - 5 days effort
      - Non-Functional
      - The database stores each created user account and the account information. That account information includes a uniquely generated ID, unique username, encrypted/hashed password, and whether or not the user is an admin for each user.
    - T1S-11.2: Query User
      - Must have
      - 3 days effort
      - Non-Functional
      - The database can return the information of a user given the user's ID or username.
    - T1S-11.3: Add User
      - Must have
      - 2 days effort
      - Non-Functional
      - The database can insert new users into the user database. This is when the user's unique ID is generated. This process must ensure that items are not added at the same time.
    - T1S-11.4: Store Product Items
      - Must have
      - 5 days effort
      - Non-Functional
      - The database stores each item that can be sold and that item's information. The item information includes a uniquely generated ID, a name, a price, a path to an image, and an optional description.
    - T1S-11.5: Query Item
      - Must have
      - 3 days effort
      - Non-Functional
      - The database can return the information of an item given the item's ID.
    - T1S-11.5: Retrieve Sellable Items
      - Must have
      - 4 days effort
      - Non-Functional
      - The database can return a list of all items that haven't yet been sold and the item's information.
    - T1S-11.6: Add Item
      - Must have
      - 2 days
      - Non-Functional
      - The database can insert new items into the item database. This is when the item's unique ID is generated. This process must ensure that items are not added at the same time.
    - T1S-11.7: Store Sales
      - Must have
      - 5 days effort
      - Non-Functional
      - The database stores each sale completed. The sale information includes the unique sale ID, the item ID, the ID of the user who bought the item, and the corresponding sale receipt.
    - T1S-11.8: Query Sale
      - Must have
      - 3 days effort
      - Non-Functional
      - The database can return the information of a sale given the sale ID or the item ID.
    - T1S-11.9: Retrieve Sales
      - Must have
      - 4 days effort
      - Non-Functional
      - The database can return a list of all sales ever made.
    - T1S-11.10: Add Sale/s
      - Must have
      - 4 days effort
      - Non-Functional
      - The database can insert new sales into the sales database. Given an order that has multiple items, a unique sale is created for each item with the corresponding item ID and user ID. The order receipt is copied into each sale in the sale database. This process must ensure that items are not added at the same time.
  - T1E-12: Product Item Page Components
    - T1S-12.1: List Product Items
      - Must have
      - 3 days effort
      - Functional
      - The page makes a database request to Retrieve Sellable Items (T1S-11.5). The items are shown in a grid.
    - T1S-12.2: Display Item Information
      - Must have
      - 3 days effort
      - Functional
      - The page shows an item's name, price, image, and description in its corresponding slot in the grid. There is also a button to add the item to the cart.
    - T1S-12.3: Filter Shown Items
      - Needs to have
      - 4 days effort
      - Functional
      - The page only shows items with a similar name to a search key when a key is entered into a provided search bar.
    - T1S-12.4: Log Out
      - Must have
      - 2 days effort
      - Functional
      - The page provides a logout button when a user is logged in. If the user presses this button, they will be logged out of their account and lose access to their shopping cart.
  - T1E-13: Login Page Components
    - T1S-13.1: Enter Login Information
      - Must have
      - 2 days effort
      - Functional
      - The page provides 2 fields that a user must fill out: a username field and a password field. The password is shown as dots unless a show password button is pressed.
    - T1S-13.2: Confirm Login
      - Must have
      - 1 day effort
      - Functional
      - The page checks the database for an account corresponding with the inputted username and password. If there is an account, log the user in and send them back to the Product List Page (T1E-7).
    - T1S-13.3: Register New Account
      - Must have
      - 1 day effort
      - Functional
      - The page provides a button that a user can press to proceed to the Register User Page (T1S-6.1).
  - T1E-14: Register User Page Components
    - T1S-14.1: Enter Login Information
      - Must have
      - 2 days effort
      - Functional
      - The page provides 2 fields that a user must fill out: a username field and a password field. The password is shown as dots unless a show password button is pressed.
    - T1S-14.2: Confirm Login Information
      - Must have
      - 2 days effort
      - Functional
      - The page provides a button to confirm login information. If the username already belongs to another user in the database, an error will be shown. If the password is less than 6 characters long, an error will be shown. If both fields are valid, a new user will be added to the database and the user will be brought back to the Product List Page (T1E-7).
  - T1E-15: Checkout Page Components
    - T1S-15.1: Show Item List
      - Must have
      - 2 days effort
      - Functional
      - The page stores a list of all items added to the cart. The items are listed on the page and a subtotal is calculated and shown.
    - T1S-15.2: Remove Items
      - Must have
      - 1 day effort
      - Functional
      - The page provides a button next to each item in the cart that, when pressed, removes the item from the cart. If the items in the cart drop to 0, the user is redirected to the Product List Page (T1E-7).
    - T1S-15.3: Pay for Items
      - Must have
      - 1 day effort
      - Functional
      - The page provides a button that takes the user to a Pay Now Page when pressed.
  - T1E-16: Pay Now Page Components
    - T1S-16.1: Enter Payment Info
      - Must have
      - 2 days effort
      - Functional
      - The page provides fields for the shipping address, phone number, credit card number, credit card expiration date, and credit card CVV number.
        - As this is a class project, these fields do not need to be checked for validity.
    - T1S-16.2: Select Shipping Option
      - Must have
      - 2 days effort
      - Functional
      - The page provides 3 options from T1S-3.3 for the user to select. The default selected option is the free option.
    - T1S-16.3: Show Order Summary
      - Must have
      - 3 days effort
      - Functional
      - The page shows the list of all items stored in the cart and their subtotal, the calculated tax, the shipping cost, and the total price.
    - T1S-16.4: Confirm Order
      - Must have
      - 1 days effort
      - Functional
      - The page provides a Confirm Order button. Once this button is pressed, and all fields are filled out, the sale will be entered into the database and the user will be taken to the Receipt Page (T1S-8.6).
  - T1E-17: Receipt Page Components
    - T1S-17.1: View Order Summary
      - Needs to have
      - 1 day effort
      - Functional
      - The page provides the information from T1S-16.3 in addition to the last 4 digits of the credit card number and the shipping address from T1S-16.1.
    - T1S-17.2: Return to Shopping
      - Needs to have
      - 1 day effort
      - Functional
      - The page provides an OK button that, when pressed, brings the user back to the Product List Page (T1E-7).
  - T1E-18: Sales Report Page Components
    - T1S-18.1: Query Sales Report
      - Must have
      - 2 days effort
      - Functional
      - The page provides a button that a user can press that queries the sales database for all sales and returns a list of sales.
    - T1S-18.2: View Sales Report
      - Must have
      - 3 days effort
      - Functional
      - The page displays a list of all sales once a sales report has been queried. When a sale is clicked, the Receipt Page (T1S-8.6) for that sale is displayed.
    - T1S-18.3: Dump Sales Report to CSV
      - Must have
      - 2 days effort
      - Non-Functional
      - The system dumps a sales report into a CSV file, maintaining the sale ID, item ID, and user ID.
    - T1S-18.4: Export Sales Report
      - Must have
      - 2 days effort
      - Functional
      - The page provides a Download Sales Report Button. When the button is pressed, the CSV file is downloaded by the client in their browser.
  - T1E-19: Add Item Page Components
    - T1S-19.1: Enter Item Info
      - Needs to have
      - 3 days effort
      - Functional
      - The page provides fields that must be filled out: item name, item price, item image, and item description.
    - T1S-19.2: Select Item Image
      - Needs to have
      - 4 days effort
      - Functional
      - The page opens a grid of all available images if a Select Item Image button is pressed. The images are pulled from a directory of images. If an image is clicked, the grid is closed and the selected image is shown. The Select Item Image button can be pressed again to select a different image.
    - T1S-19.3: Confirm Item
      - Needs to have
      - 2 days effort
      - Functional
      - The page allows a Confirm Item button to be pressed once all fields are filled. Once the button is pressed, the item is inserted into the database. The fields are cleared so that a user can repeat the process.
  - TIE-20: Common Webpage Components
    - T1S-20.1: Travere to Product List Page
      - Must have
      - 1 day effort
      - Functional
      - The page provides a Home button that can be pressed to traverse to the Product List Page (T1E-7).
    - T1S-20.2: Traverse to Login Page
      - Must have
      - 1 day effort
      - Functional
      - The page provides a Login button that can be pressed to traverse to the Login Page (T1S-6.2). When is user is logged in, this button turns into a Logout button.
    - T1S-20.3: Traverse to Checkout Page
      - Must have
      - 1 day effort
      - Functional
      - The page provides a Checkout button that can be pressed to traverse to the Checkout Page (T1E-8). This button can only be pressed if a user is logged in and has at least one item in their cart.
    - T1S-20.4: Traverse to Sales Report Page
      - Needs to have
      - 1 day effort
      - Functional
      - The page provides a Sales Report button that can be pressed to traverse to the Sales Report Page (T1E-9). This button can only be seen if the user is an admin.
    - T1S-20.5: Traverse to Item Creation Page
      - Needs to have
      - 1 day effort
      - Functional
      - The page provides a Create new Item button that can be pressed to traverse to the Create new Item Page (T1E-10). This button can only be seen if the user is an admin.

## Version 2
- Milestone: Future
  - T1E-: Additional User Information
    - T1S-: Save Email
      - Wants to have
      - 1 day effort
      - Functional
      - A user can optionally provide their email address during account creation. This value is saved in the database.
    - T1S-: Save Phone Number
      - Wants to have
      - 1 day effort
      - Functional
      - A user can optionally provide their phone number during account creation. This value is saved in the database.
    - T1S-: Save Shipping Info
      - Wants to have
      - 1 day effort
      - Functional
      - A user can have their shipping info saved during checkout for future use.
    - T1S-: Save Payment Method
      - Wants to have
      - 1 day effort
      - Functional
      - A user can opt to have their payment method saved for future purchases.
  - T1E-: User Account Page
    - T1S-: Enter Additional Information
      - Wants to have
      - 2 days effort
      - Functional
      - A user can enter/edit their email or phone number for future use.
    - T1S-: Change Username
      - Wants to have
      - 2 days effort
      - Functional
      - A user can change their username to a new unique username.
    - T1S-: Change Password
      - Needs to have
      - 2 days effort
      - Functional
      - A user can change their password. Doing so requires the current password to be inputted. If the user does not know their password, an account recovery page can be accessed.
    - T1S-: Delete Account
      - Wants to have
      - 3 days effort
      - Functional
      - The page allows for a user to permanently delete their account.
    - T1S-: Scrub Shipping & Payment Info
      - Needs to have
      - 2 days effort
      - Functional
      - A user can delete their saved shipping info and payment info if they no longer want it saved.
  - T1E-: Account Recovery
    - T1S-: Recover Password
      - Needs to have
      - 5 days effort
      - Functional
      - A user can request a password reset via email.
    - T1S-: View Password Reset Page
      - Needs to have
      - 3 days effort
      - Functional
      - A user can input a new 6-character or more password.
  - T1E-: Product List Page QOL
    - T1S-: Display Multiple Item Images
      - Wants to have
      - 3 days effort
      - Functional
      - Items can have multiple images uploaded. A user can cycle through item images on the Product List Page (T1E-7).
    - T1S-: Modify Existing Product Item
      - Wants to have
      - 4 days effort
      - Functional
      - An admin can modify any of the fields of an item in the database.
  - T1E-: Checkout QOL
    - T1S-: Deselect Items in Cart
      - Wants to have
      - 1 day effort
      - Functional
      - A user can uncheck an item in their cart to unselect them. When proceeding with payment, only selected items are included in checkout.
    - T1S-: Email Receipt to User
      - Needs to have
      - 2 days effort
      - Functional
      - The system emails an order receipt to the user when an order is completed.
    - T1S-: Store Current Cart
      - Wants to have
      - 4 days effort
      - Non-Functional
      - The system stores the user's current cart in the database so that it can be loaded if a user leaves the site or logs out and back in again.
  - T1E-: Allow New Item Entering QOL
    - T1S-: Upload New Item Images
      - Wants to have
      - 4 days effort
      - Functional
      - An admin can upload new images for items being added to the database.
  - T1E-: Admin Promotion Page
    - T1S-: Promote User to Admin
      - Wants to have
      - 3 days effort
      - Functional
      - The page provides a way for admins to search for a user and then promote them to admin.
