# C# Pool Project - Tetris
Matthias Flament - Fabian Ingremeau - Léo Séry

## Summary 

- Presentation
- How to install project 
- How to use of the project 
- Encountered difficulties


## Presentation :

The goal of this project was to create a ***Tetris*** using the **c#** language. So we used the **UWP** library to use **xaml** files with our *C#*.
We were instructed to add some of the following features:

- Ability to **turn the pieces**
- Have a **Timer** 
- Show the **next room**
- **Delete line** when complete
- Add ability to **change music**
- Ability to **configure keys** 

## How to install project :

- Clone the git repository to your computer with the following command :
```
https://github.com/Carpentic/TetrisB2.git
```
or 
```
git@github.com:Carpentic/TetrisB2.git
```

## How to use of the project : 

Once the project is cloned on your machine, open the solution with Visual Studio.

If you have problems opening the solution, do: `Build > Rebuild Solution`

To run the project click on: `Local Computer`

Once the application is launched, you have the choice between three options: 

- Play 
- Options 
- Quit 

### **Play :** 

By default you can **use the arrow keys** to turn the tetrominoes. *(You can change the keys in the options menu)*

At the bottom left you can **adjust the game volume** as well as return to the main menu.

To the left of the volume you can see the **time elapsed** since the start of the game.

And in the middle of the window under the logo you have a **preview of the next piece** as well as your score.

### **Option :**

At the top of this page you have the possibility **to change the key assignment** by clicking on the box corresponding to the action you want assigned and then pressing the key you want to use.

Following as in the game window you have the possibility to **adjust the volume**.

*"Grid reset score"* corresponds to the value from which you want the grid to reset.

And finally you have the possibility to **choose an audio** file to **replace the base soudtrack**. (you can upload among the following extensions: *".mp3"*, *".wma"*, *"wav"*, *".ogg"*).

## Encountered difficulties :

The main difficulty in this project is that we had never used UWP in the C# pool or even seen a graphic part, which slowed us down quite a bit, even if the algorithm and the project itself were quite simple. 

