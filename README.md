# TicTacToeOOP 🎮

A simple **Tic-Tac-Toe** console game written in C# using **object-oriented programming** principles.  
Built as a learning project to practice classes, interfaces, encapsulation, and method structure in .NET 8.

---

## 🚀 Features

- Two-player mode (human vs human)
- Clean, object-oriented structure:
  - `Board` manages the grid and game logic
  - `Game` handles the main loop and flow
  - `IPlayer` defines a common interface for all player types
  - `HumanPlayer` reads input from the console
- Fully functional win and draw detection
- Easily extendable (add AI or a graphical interface later)

---

## 🧠 Future Plans

- Add a **ComputerPlayer** (AI opponent)  
- Create a **Unity** version with GUI, sounds, and animations  
- Implement save/load and replay features  

---

## 🛠️ Built With

- **C# 12 / .NET 8**
- **Visual Studio 2022 Community**
- Console-based user interface

---

## 🧩 How to Run

1. Clone the repo:
   ```bash
   git clone https://github.com/andersabjorn/TicTacToeOOP.git
   Open TicTacToeOOP.sln in Visual Studio

Press Ctrl + F5 to start the game
📄 License

This project is released under the MIT License.
Feel free to use, modify, and learn from it.

👤 Author

Anders Åbjörn
GitHub: @andersabjorn


---

## ⚙️ **2. .gitignore**

Create another file named **`.gitignore`**  
Paste this in (standard for C# / .NET projects):

```gitignore
# Build results
bin/
obj/
Debug/
Release/

# User-specific files
*.user
*.rsuser
*.suo
*.userosscache
*.sln.docstates

# Visual Studio cache
.vs/

# Temporary files
*.tmp
*.log

# Auto-generated files
*.nupkg
*.snupkg
*.dbmdl

# OS files
.DS_Store
Thumbs.db

