#r @"packages\FAKE\tools\FakeLib.dll"

open Fake
open System.IO

let remove str (target: string) =
  target.Replace (str, "")

let update filename content =
  File.Delete filename
  File.WriteAllText (filename, content)

!! "src/**/*.csproj"
|> Seq.iter (fun fileName ->
      fileName 
      |> File.ReadAllText
      |> remove @"<Import Project=""$(SolutionDir)\.nuget\NuGet.targets"" Condition=""Exists('$(SolutionDir)\.nuget\NuGet.targets')"" />"
      |> update fileName
)

