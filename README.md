# 2022 Advent of Code (C#)

This repository implements solutions to the puzzles in the
[2022 Advent of Code](https://adventofcode.com/2022) using C#.


## Preface

This was a vehicle to learn C#, so I presume not everything done here will
be deemed idiomatic by language specialists.

Generally speaking, the solutions are organised predominantly for comprehension.
They strive to arrive at an answer in a reasonable period of time, but they
typically prioritise optimal understanding over optimal performance.

The examples are representative of my thinking and coding style.


## Getting Started

### Prerequisites

The project requires `.Net 8.0`, but any reasonably current version of
.Net will likely work. I tend to code done the middle of any language
specification.

If you use a .Net manager that responds to `.tool-versions`, you should
be switched to correct version automatically. I recommend [ASDF](https://github.com/asdf-vm/asdf)
for those on platforms that support it.

### Installation

The project has a small `Makefile` that scripts common build, run, and test
commands.

```
$ make build
$ make clean
$ make run
$ make verify
```

### File Structure

- [app](./app):     A console app that invokes a given day's runner.
- [data](./data):   Puzzle input organised by day
- [lib](./lib):     Homegrown utilities
- [site](./site):   A local version of the instruction pages
- [src](./src):     Puzzle solutions organised by day
- [test](./test):   A simple set of regression tests


### Running Daily Solutions

Modify the code in the simple console application in `/app/Program.cs`.

Then, simply execute the following command in your terminal from the
project root.

```
$ make run
```

### Running Tests

The only tests are a set of checks to verify solved puzzles.

I often refactor my solutions for clarity (or as I learn new
techniques in subsequent puzzles), so it is helpful to have
these simple tests to give my refactors some confidence.

To execute the tests, simply execute the following command in
your terminal from the project root.

```
$ make verify
```