/////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var projectDirectory = Directory("../Json.Loader");
var testDirectory = Directory("../Json.Loader.Tests");
var solution = "../Json.Loader.sln";

//////////////////////////////////////////////////////////////////////
// SETTINGS
//////////////////////////////////////////////////////////////////////

var cleanSettings = new DotNetCoreCleanSettings
{
    Framework = "netcoreapp2.0",
    Configuration = configuration
};

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Description("Cleaning the build directory.")
    .Does(() =>
    {
        DotNetCoreClean(projectDirectory, cleanSettings);
        DotNetCoreClean(testDirectory, cleanSettings);
    });

Task("Restore")
    .Description("Restoring NuGet packages.")
    .Does(() =>
    {
        DotNetCoreRestore(solution);
    });

Task("Build")
    .Description("Building the solution.")
    .Does(() =>
    {
        DotNetCoreBuild(solution);
    });

Task("Test")
    .Description("Runs Unit tests.")
    .Does(() =>
    {
        DotNetCoreTest(testDirectory);
    });

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("Test")
    .Finally (() =>
    {
        Information("Complete");
    });

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);