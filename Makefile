build:
	dotnet build
clean:
	dotnet clean
run:
	dotnet run --project app/Aoc.Cli/Aoc.Cli.csproj
verify:
	dotnet test