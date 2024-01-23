# Gym Management System - C# Console Application

## Overview

Welcome to the Gym Management System! This console application allows users to manage various aspects of a gym, including members, coaches, memberships, programs, payments, and user accounts. This application interacts with a SQL database to manage previous models, you'll find information on how to use the different features of the application.

## Technologies Used
- **Language:** C#
- **Database:** SQL Server
- **Data Access:** ADO.NET
- **Console Interface:** System.Console

## Getting Started
To use the Gym Management System, follow these steps:

1. **Clone the Repository:**
Clone this repository to your local machine using the following command:
    ```bash
        git clone https://github.com/Mostafa-ElMonateh/Gym-Management-System.git
    ```

2. **Database Setup:**
- Create a `SQL Server database` and run the SQL scripts provided in the sql-scripts directory to set up the necessary tables.

3. **Open the Solution:**
Open the solution file (`.sln`) in your preferred C# IDE (e.g., Visual Studio).

3. **Run the Application:**
Build and run the application to start managing your gym.

## User Authentication
Upon launching the application, the user will be prompted to enter their username and password to access the features.

Some Of User Accounts:
- username = `sara`, password = `1592001`
- username = `mostafa`, password = `591998`

## Features
**Members**
- *Add a New Member:*
    - Allows the user to add a new member to the system.
- *Search for a Member:*
    - Enables the user to search for a specific member by his phone number.
- *Update Member Info:*
    - Allows the user to update information for a specific member.
- *Add Program to Member:*
    - Enables the user to associate a program with a specific member.

**Coaches**
- *Add a New Coach:*
    - Allows the user to add a new coach to the system.
- *Search for a Coach:*
    - Enables the user to search for a specific coach by ID.
- *Update Coach Info:*
    - Allows the user to update information for a specific coach.
- *Delete a Coach:*
    - Enables the user to delete a coach from the system.

**Memberships**
- *Add a New Membership:*
    - Allows the user to add a new membership to the system.
- *Update Membership:*
    - Enables the user to update information for a specific membership.
- *Show All Memberships:*
    - Displays a list of all active memberships.
- *Delete Membership:*
    - Enables the user to delete a specific membership.
- *Show Deleted Memberships:*
    - Displays a list of all deleted memberships.

**Programs**
- *Add a New Program:*
    - Allows the user to add a new program to the system.
- *Update Program:*
    - Enables the user to update information for a specific program.
- *Show All Programs:*
    - Displays a list of all available programs.
- *Delete Program:*
    - Enables the user to delete a specific program.
- *Show Deleted Programs:*
    - Displays a list of all deleted programs.

**Payments**
- *Show All Payments:*
    - Displays a list of all payments made.
- *Show Payments for a Specific Member:*
    - Enables the user to view payments for a specific member.

**Users**
- *Show All Users:*
    - Displays a list of all user names.
- *Update User Info:*
    - Allows the user to update their own information (username and password).

## Acknowledgments

- Special thanks to [Mostafa Saqaly](https://www.linkedin.com/in/mostafa-saqly/), our instructor in ITI, for providing guidance and support throughout the development of this project.

- Special thanks to [Sara Mohamed](https://github.com/Saramohamed13), [Ziad Hakem](https://github.com/ZiadHakem) and [Nada Assem](https://github.com/Nada-Assem) my contributors, for their valuable contributions.