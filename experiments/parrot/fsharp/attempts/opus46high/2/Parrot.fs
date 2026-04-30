module Parrot

type ParrotType =
    | European
    | African
    | NorwegianBlue

let speed parrotType numberOfCoconuts voltage isNailed =
    match parrotType with
    | European -> 12.0
    | African -> max 0.0 (12.0 - 9.0 * float numberOfCoconuts)
    | NorwegianBlue ->
        if isNailed then 0.0
        else min 24.0 (voltage * 12.0)

let cry parrotType voltage =
    match parrotType with
    | European -> "Sqoork!"
    | African -> "Sqaark!"
    | NorwegianBlue ->
        if voltage > 0.0 then "Bzzzzzz"
        else "..."
