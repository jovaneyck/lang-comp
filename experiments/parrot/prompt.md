# Your goal

You are implementing a new type of parrot, the "Exotic Parrot", it has a constant speed of 4 and a cry of "squaaaawk-exotic". First refactor the code, make it open-closed and use factory method, it should be straightforward to add new parrot types in the future.

You are given a path to an attempt directory. Read the readme.md for that attempt.
You start from an existing implementation with accompanying test cases, all are passing from the start.
As a first TODO item, get the start_time (Get-Date -Format "yyyy-MM-dd HH:mm:ss.fff")
Keep track of the number of iterations you need (failing builds or failing test runs) until you get to green with all acceptance criteria met.
As second-to-last TODO item, get the end_time (Get-Date -Format "yyyy-MM-dd HH:mm:ss.fff")
As last TODO item, write a file in the current attempt folder called log.csv with 

```csv
tokens(fill with hardcoded zero);start_time;end_time;number_of_builds_test_runs_needed;<single-sentence description of how the run went, highlighting any surprises or challenges>;
```

## Acceptance criteria

- All existing tests still pass (no regressions)
- The code follows the Open-Closed Principle: adding a new parrot type does not require modifying existing code
- A factory method is used to create parrot instances
- A new "Exotic Parrot" type is added with:
  - Speed: always 4
  - Cry: "squaaaawk-exotic"
- Tests are added for the Exotic Parrot's speed and cry
