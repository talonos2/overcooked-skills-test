# overcooked-skills-test

A "Programmer art" recreation of overcooked, done as a skills assessment for a programming position.

Playable but incomplete. You mentioned that "the objective of the test is to assess your code quality and technical understanding. Not necessarily to complete the game or all the functionalities," so I omitted parts because of time restraints. I worked a total of 12 hours on it and implemented the following:

- Chefs move around the field. (WASD for the Blue chef, Warrow keys for the Red Chef.)
- Colored ingredients spawn on the left and right sides of the screen.
- Chefs can use the Control key on their side of the keyboard to pick up ingredients. A chef can carry up to two.
- With a held ingredient, a chef can press control again to put it on a placable surface, including a countertop, a cutting board, or a trash can.
- Placing an incredient on the cutting board causes the chef to enter a chopping animation, freezing them momentarily.
- Trash cans void ingredients
- Countertops store ingredients, and you can retrieve the ingredient later.
- After a cutting board has been given an ingredient, it then contains a "salad", represented by a colored plate.
- You can add ingredients to a salad, but no more than one of the same type.
- You can pick up the salad if your hands are empty.
- You can either void a salad in the trash, place it on the countertop for whatever reason, or put it on one of the gold delivery tables at the top of the screen.
- If the salad plate is the same color as the rotating disc above the delivery table, you are rewarded with green fireworks.
- Otherwise, you are given red fireworks.

The following was unimplemented due to time constraints.

- There is no UI. You can tell what ingredients you are holding because your chef holds them in his hands.
- There is no scoring system.
- Playtime is unlimited.
- Customers will not leave
- There are no powerups.
- There is no music and there are no sound effects. (You never asked for them, but they add so much to a game that it feels incomplete without them, and I'd have added them in given enough time.)

If I were to continue:

- I would start by implementing the missing requests, starting with customers leaving, then powerups, then score, then time limit, then all remaining UI.
- I would add an Esc menu that allows you to reset the game and view controls
- I would refine the way the carriables attach to the character's hands (particularly the salads.)
- I would add sound effects and music, particularly adding music that layers on additional instrumentation when the timer is low.
