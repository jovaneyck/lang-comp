# Your goal

You are refactoring the Gilded Rose kata and adding a new feature.
You are given a path to an attempt directory. Read the readme.md for that attempt.
You start from an existing implementation with accompanying characterization tests, all are passing from the start.
As a first TODO item, get the start_time (Get-Date -Format "yyyy-MM-dd HH:mm:ss.fff")
Keep track of the number of iterations you need (failing builds or failing test runs) until you get to green with all acceptance criteria met.
As second-to-last TODO item, get the end_time (Get-Date -Format "yyyy-MM-dd HH:mm:ss.fff")
As last TODO item, write a file in the current attempt folder called log.csv with 

```csv
tokens(fill with hardcoded zero);start_time;end_time;number_of_builds_test_runs_needed;<single-sentence description of how the run went, highlighting any surprises or challenges>;
```

## Task

You MUST refactor the existing Gilded Rose code to improve its design and maintainability. Keep the design idiomatic to the programming language we are using (inheritance and polymorphism vs. pattern matching and union types).
Implement the "Conjured" item feature as described below.
Do not alter the Item type — the goblin in the corner will insta-rage.

## New feature request

Implement support for "Conjured" items. Conjured items degrade in Quality twice as fast as normal items.

## Acceptance criteria

### Conjured item before sell date
- Conjured item (sellIn=10, quality=20) after one update → (sellIn=9, quality=18)

### Conjured item on sell date
- Conjured item (sellIn=0, quality=20) after one update → (sellIn=-1, quality=16)

### Conjured item after sell date
- Conjured item (sellIn=-5, quality=10) after one update → (sellIn=-6, quality=6)

### Conjured item quality never negative
- Conjured item (sellIn=5, quality=1) after one update → (sellIn=4, quality=0)
- Conjured item (sellIn=0, quality=1) after one update → (sellIn=-1, quality=0)
- Conjured item (sellIn=0, quality=3) after one update → (sellIn=-1, quality=0)

### All existing characterization tests still pass
- The refactoring must not change any existing behavior
