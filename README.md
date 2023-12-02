# Assembly Line Operations Manager - Blazor Server App


### Introduction
This repository hosts a Blazor Server application that streamlines the management of assembly line operations. It's a simple yet effective tool for assigning and tracking devices used in various operations within an assembly line. Built in Visual Studio Community Edition, it's a showcase of clean architecture and practical design in a web application.

### Key Features
Operations Listing: Easily view all the operations, with functionality to remove any as needed.
Adding Operations: Through a user-friendly modal, new operations can be added, ensuring they fit perfectly in the existing sequence.
Device Assignment: Assign devices to operations with another straightforward modal interface.
Intuitive UI: A clean, easy-to-navigate user interface, crafted with a combination of Bootstrap, HTML, and CSS.

### Why This Approach?
#### Clean Architecture
I chose clean architecture for its robustness in maintaining separation of concerns. This makes the application scalable and easy to manage, which is essential for adapting to future changes or expansions.

#### Repository Pattern and In-Memory Database
The repository pattern is a key part of the architecture, and it's utilized uniquely in this project. I've created a plugin where the in-memory database is stored. This design choice allows for flexibility in data management, where another plugin for a persistent database can easily be swapped in. It's a setup that anticipates the need for switching between an in-memory system and a more permanent database solution, providing a seamless transition when scaling up.

#### Validation Rules
Ensuring that operations are added in a specific sequence is critical for reflecting real-world scenarios. I've implemented validation rules that enforce this, maintaining the integrity and practicality of the application in managing assembly line processes.

#### DTOs and Asynchronous Operations
Data Transfer Objects (DTOs) are used throughout the application to handle data efficiently, improving performance and enhancing security. Despite the current setup using an in-memory database, I've implemented asynchronous operations across the application. This choice keeps the application consistent and ready for potential integration with an external database. It's about future-proofing the app, ensuring that it can handle more complex, real-world data operations without needing significant restructuring.
### Extras
Assembly Integration: The ability to group operations into assemblies adds another layer of organization, reflecting real-world assembly line scenarios.
Font Awesome: To enhance the UI, Font Awesome icons are incorporated, giving a modern and professional look to the application.
