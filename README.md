# kryptonite
A C# parser combinator based on the ideas expressed in Superpower and Sprache. This is me exploring and reimplementing the ideas for educational pursposes, but also these two projects appear abandoned and it has been at least a year since either project has had any activity. So maybe with the magic of dotnet 8 & C# 12, I can build something similar to Superpower while making use of Span<T> and ref structs to minimize allocation.

## refrences
- Graham Hutton's & Erik Meijer's paper on monadic parser combinators: https://www.cs.nott.ac.uk/~pszgmh/monparsing.pdf
- Nicholas Blumhardt's Superpower - https://github.com/datalust/superpower
- Nicholas Blumhardt NDC Conference Superpower demo - https://www.youtube.com/watch?v=klHyc9HQnNQ
