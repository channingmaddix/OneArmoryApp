# OneArmoryApp
One Armory is designed to give Army company commanders and armorers the ability to better track armory weapon readiness and maintenance.
The first page the user is brought is focused on giving the commander a snapshot of the readiness level of his/her armory. 

Current functionality allows for:

-CRUDing of weapons, soldiers, and work orders

-Home page filtering of weapon readiness levels based on:

    +weapon type
    +platoon
    

Future functionality will include:

-Single role authentication

-Time and user stamped records of changes

-Tracking of equipment signed out to soldiers

-Autofilling and printing of workorders and fhand receipts


Requirements:
---
Users must use either a Windows desktop or have an instance of Windows on their machine.
Users must have either SQL Server Management Studio or use Visual Studio's default SQL Server installed.
For setup, users must run provided post-deployment script for creation of tables and populating test data. 

Class Diagram:
---
![Class Diagram](https://github.com/channingmaddix/OneArmoryApp/blob/master/ClassDiagram.jpg?raw=true)


State Diagram:
---

![State Diagram](https://github.com/channingmaddix/OneArmoryApp/blob/master/StateDiagram.jpg?raw=true)


Use Case Diagram:
---
![Use Case Diagram](https://github.com/channingmaddix/OneArmoryApp/blob/master/UseCaseDiagram.jpg?raw=true)

Home Page:
---
Users can filter by platoon and weapon type to see the readiness level of the weapons in the system.
The readiness bars and filters auto-populate based on what is currently in the system. 
Bar colors are dependent on the readiness level of the weapon type. 
The buttons on the left can be used to access weapon, soldier, and work order CRUD pages.
![Home Page](https://github.com/channingmaddix/OneArmoryApp/blob/master/HomePage.jpg?raw=true)

CRUD Page:
---
Users can choose options from the right to RUD a weapon. 
The checkboxes on the left can be used to create a mass work order.
Checkboxes will be given deletion functionality in a future update.

![CRUD Page](https://github.com/channingmaddix/OneArmoryApp/blob/master/WeaponIndexPage.jpg?raw=true)
