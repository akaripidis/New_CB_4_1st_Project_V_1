# New_CB_4_1st_Project_V_1
This project was requested as part of a very intensive seminar program that i attended. 
It is a single console application that it should register users in order to give them 
the ability to exchange messages. You should also be able to assign different responsibilities
to the user through out an admin account. It should store and retrieve information from a database.
This program was written without any previews coding experience. 
It uses no design patterns and it has repetitive code.
It was like an experimenting step pray to work kind of situation, implementing heavy testing in every
step to find bugs and understand hoe exactly compiler fulfils code and what object oriented programming
is all about. It is written with in a tight time-schedule. In order to minimize database hits it uses 
generic lists updated during start up. When a change happens it updates both the list and database. 
This way when a user requests data no call to the database has to be done. User names are unique, passwords
are hashed and stored twice in the database using different techniques for hashing.  Messages are deleted when
both sender and receiver has deleted the message. Enjoy!
