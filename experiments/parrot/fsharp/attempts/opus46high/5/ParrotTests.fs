module ParrotTests

open Xunit
open FsUnit.Xunit

[<Fact>]
let ``speed of european parrot`` () =
    Parrot.speed Parrot.European 0 0.0 false |> should equal 12.0

[<Fact>]
let ``speed of african parrot with one coconut`` () =
    Parrot.speed Parrot.African 1 0.0 false |> should equal 3.0

[<Fact>]
let ``speed of african parrot with two coconuts`` () =
    Parrot.speed Parrot.African 2 0.0 false |> should equal 0.0

[<Fact>]
let ``speed of african parrot with no coconuts`` () =
    Parrot.speed Parrot.African 0 0.0 false |> should equal 12.0

[<Fact>]
let ``speed of norwegian blue parrot nailed`` () =
    Parrot.speed Parrot.NorwegianBlue 0 0.0 true |> should equal 0.0

[<Fact>]
let ``speed of norwegian blue parrot nailed with voltage`` () =
    Parrot.speed Parrot.NorwegianBlue 0 1.5 true |> should equal 0.0

[<Fact>]
let ``speed of norwegian blue parrot not nailed`` () =
    Parrot.speed Parrot.NorwegianBlue 0 1.5 false |> should equal 18.0

[<Fact>]
let ``speed of norwegian blue parrot not nailed high voltage`` () =
    Parrot.speed Parrot.NorwegianBlue 0 4.0 false |> should equal 24.0

[<Fact>]
let ``cry of european parrot`` () =
    Parrot.cry Parrot.European 0.0 |> should equal "Sqoork!"

[<Fact>]
let ``cry of african parrot`` () =
    Parrot.cry Parrot.African 0.0 |> should equal "Sqaark!"

[<Fact>]
let ``cry of norwegian blue parrot high voltage`` () =
    Parrot.cry Parrot.NorwegianBlue 4.0 |> should equal "Bzzzzzz"

[<Fact>]
let ``cry of norwegian blue parrot no voltage`` () =
    Parrot.cry Parrot.NorwegianBlue 0.0 |> should equal "..."
