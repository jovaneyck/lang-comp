# Your goal

You are implementing a functional change to the Game of Life. 
You are given a path to an attempt directory and your model_name. Read the readme.md for that attempt.
You start from an existing implementation with accompanying test cases, all are passing from the start.
As a first TODO item, get the start_time (Get-Date -Format "yyyy-MM-dd HH:mm:ss.fff")
Keep track of the number of iterations you need (failing builds or failing test runs) until you get to green with all acceptance criteria met.
As second-to-last TODO item, get the end_time (Get-Date -Format "yyyy-MM-dd HH:mm:ss.fff")
As last TODO item, write a file in the current attempt folder called log.csv with 

model_name;start_time;end_time;number_of_builds_test_runs_needed;

## New feature request

Introduce a new kind of cell state, called "zombie". A zombie cell behaves as if it is both dead and alive at the same time.
It counts as a live cell for the purposes of determining the next state of its neighbors, but it does not change its own state in response to its neighbors. In other words, a zombie cell will always remain a zombie cell, regardless of how many live neighbors it has.

## Acceptance criteria

### Zombie immutability
- Zombie with 0 live neighbors → stays zombie
- Zombie with 3 live neighbors → stays zombie
- Zombie with 4+ live neighbors → stays zombie

### Zombie counts as live for neighbor birth rule
- Dead cell with exactly 3 zombie neighbors → becomes alive

### Zombie counts as live for neighbor survival
- Live cell with exactly 2 zombie neighbors → stays alive

### Zombie counts as live for overcrowding
- Live cell surrounded by zombies (4+) → dies

### Mixed grid (0 = dead, 1 = alive, Z = zombie)

Before:
```
0 0 1 0 0 0 Z 0 0
0 0 0 1 0 0 0 0 0
0 1 1 1 0 0 0 0 0
0 0 0 0 0 0 0 0 0
Z 0 0 0 0 0 0 0 Z
0 0 0 0 0 0 0 0 0
0 0 0 0 0 1 1 1 0
0 0 0 0 0 1 0 0 0
0 0 Z 0 0 1 0 Z 0
```

After 1 tick:
```
0 0 0 0 0 0 Z 0 0
0 1 0 1 0 0 0 0 0
0 0 1 1 0 0 0 0 0
0 1 1 0 0 0 0 0 0
Z 0 0 0 0 0 0 0 Z
0 0 0 0 0 0 1 1 0
0 0 0 0 0 1 1 0 0
0 0 0 0 1 1 0 1 0
0 0 Z 0 0 0 1 Z 0
```
