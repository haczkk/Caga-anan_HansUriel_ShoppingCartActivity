# Progress for ShoppingCartSystem

• Completed the requirements for the project (Product class, Fields, and Methods).

• Used a constructor for ease of data storing.

• Made changes in the syntax of requirements.

• Added cart logic.

• Added fixed-sized array to hold cart items.

• Added main shopping loop, menu display, and product selection with validation.

• Added quantity input and prevention of duplicate entries.

• Added a logic where you can’t buy more than the available stock and prevention of adding new items when cart is full.

• Finalized the add to cart logic.

• Finalized the receipt generation.

• Added discount check logic.

• Finalized the displayed updated stocks after purchase.

• Added the final message.

• Fixed the alignment of displayed messages.

• Shopping Cart System Finished.

# Progress for EnhancedShoppingCartSystem

• Made the Y/N validation stronger.

• Separated the classes into different source code files.

• Added a Category field to the Product.cs

• Added a separate class file for Receipt generation.

• Used const for declaring the value of the conditions so it cannot be changed during the program's execution.

• Made the arrays (Product menu, Cart, Order History) static so it could be accessed by all methods that will be implemented in the project.

• Added the Main Menu.

• Added the product catalog menu and shopping navigation.

• Finalized the function that adds items to the cart.

• Implemented input validations and cart logics to the AddItemToCart() function.

• Added the Search Product system.

• Implemented a logic where it finds a product even if only part of the name is entered.

• Finalized the Category Filtering system (with added "Show All" option).

• Added the Cart Management Menu.

• Added the ViewCart() function to view the products in the cart. It also checks if cart has no items.

# Problem

• Encountered a problem with not properly declared variables. (SOLVED)

• Encountered a problem with negative stocks where you could still buy even if the stock is less than you would purchase. (SOLVED)

• Encountered a problem with Y/N validation where it accepts any input except "Y" instead of "Y" and "N" only. (SOLVED)

• "The code keeps Product, CartItem, and Program in a single Program.cs file." (SOLVED)

• All shopping features were originally inside one large method. (NOT YET SOLVED)

# AI Usage in This Project

• “Why am I getting an error with my variable not being declared?"

    – Used it to determine errors when it comes to properly declaring variables, which I applied throughout my program.
    
• “How do I validate user input in C# using int.TryParse to avoid errors?”

    – For implementing input validation using int.TryParse, ensuring that invalid inputs do not crash the program.

• “How do I prevent users from entering negative or zero values in C#?”

    – For adding conditional checks to apply valid input.

• "Explain how separation of files when classes work in C#. Make it beginner-friendly."

    - Used it as a guide for addressing my problem with classes being in the same source code.

• "What functions can I use to cleanly format the output in C#?"

    - Helped me with formatting issues during the development of this program. More likely for future changes on this project as well.

• "How do I separate my shopping system features into methods to make my code cleaner and easier to maintain?"

    - Used this prompt as a guide to help me separate the methods that i used and will use in this project.

• "When is the correct point to deduct product stock so inventory stays accurate and only updates after a successful add?"

    - Used it for maintaining accurate inventory after adding items.

• "I want to improve the search so users can find products even if they only type part of the product name. What would work best for partial matching?"

    - Used it to improve product search flexibility.
