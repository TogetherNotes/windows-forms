# TogetherNotes Windows Forms Application

**TogetherNotes** is a desktop application developed in **C#** with an integrated **SQL Server database**, designed to manage the connection between independent musicians and venues like bars and restaurants. The app is built to support three user roles, each with specific permissions for managing musicians, venues, and user data.

## Features

### **User Roles & Permissions**
**Superuser**:
  - Full access to all functionalities.
  - Can create, modify, and delete any user (superuser, administrator, or data maintenance).
  - Complete management of musicians and venues.
  
**Administrator**:
  - Can create and delete musicians and venues.
  - Can modify data for musicians and venues.
  - Cannot manage other users in the desktop application.

**Data Maintenance User**:
  - Can only modify existing data for musicians and venues.
  - Cannot create or delete musicians, venues, or other application users.

### **App Extensions**
**Internal Notifications**: Receive alerts and notifications about changes in data (via email or internal alerts when a user reopens the app).
**Event Calendar**: Organize and manage musician performances and events.
**Reports & Statistics**: Generate reports and performance statistics for musicians and venues.
**Map Integration**:
  - Display venue locations on a map to help musicians plan their travel.
  - Allow musicians to mark areas where they are available to perform.
**Version Control & Audit Log**:
  - Record all actions performed by users (data changes, deletions, and creations).
  - Maintain a modification history to recover deleted information.
**Multilingual Support**: Available in Catalan, Spanish, and English.
**Integrated Help**:
  - Interactive user guide to assist with using the app.
  - In-app technical support for queries or incidents.

## Technologies Used

**C#**: The primary programming language for building the application.
**SQL Server**: Used for the backend database to store user data, performances, contracts, and more.
**Windows Forms**: For creating the desktop interface of the application.
  
## Installation

1. Clone the repository:
   bash
   git clone https://github.com/TogetherNotes/windows-forms.git
   
2. Open the project in **Visual Studio**.

3. Ensure that **SQL Server** is set up and configure the database as per the provided SQL script.

4. Build and run the application.

## Setup SQL Server

You can set up the database using the provided SQL script. Follow the instructions in the **SQL script** to create the database and tables.

## Contributing

We welcome contributions! Feel free to fork the repository, submit issues, or create pull requests to improve the application.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contact

If you have any questions or suggestions, feel free to contact us at 148581386+rwxce@users.noreply.github.com.
