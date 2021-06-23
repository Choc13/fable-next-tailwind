module Pages.Index

open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props

type SectionType =
    | Even
    | Odd

type SectionContent =
    { Title: ReactElement
      Body: ReactElement }

type Section =
    { Type: SectionType
      Key: string
      Content: SectionContent }

let private section
    { Type = typ
      Content = content
      Key = key }
    =
    section [ ClassName(
                  [ "container text-center py-20 space-y-20"
                    match typ with
                    | Even -> "text-primary"
                    | Odd -> "text-secondary" ]
                  |> String.concat " "
              )
              Key key ] [
        h3 [] [ content.Title ]
        p [] [ content.Body ]
    ]

let mkSections =
    Seq.mapi
        (fun i s ->
            { Content = s
              Key = string i
              Type = if i % 2 = 0 then Even else Odd }
            |> section)

let private loremIpsum =
    "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut
labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco
laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in
voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat
non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."


let home () =
    fragment [] [
        header [ ClassName "container text-center py-20 lg:py-40 space-y-16" ] [
            h1 [] [
                str "Run your code forall inputs"
            ]
            h2 [ ClassName "text-secondary" ] [
                str "Try Symbolica and see what you find"
            ]
            div [ ClassName "flex items-center justify-center space-x-5" ] [
                a [ Href "#signup" // TODO: Make this a next link
                    ClassName
                        "no-underline px-4 py-2 rounded-full text-2xl border-2 border-secondary hover:bg-purple-light" ] [
                    str "Join Alpha"
                ]
                a [ Href "/playground" // TODO: Make this a next link
                    ClassName
                        "no-underline px-4 py-2 rounded-full text-2xl text-white bg-secondary hover:bg-purple-light" ] [
                    str "Try Free"
                ]
            ]
        ]
        main
            []
            ([ { Title = str "What is Symbolica?"
                 Body = str loremIpsum }
               { Title = str "Try it now"
                 Body = str loremIpsum }
               { Title = str "Run on every commit"
                 Body = str loremIpsum }
               { Title =
                     span [] [
                         str "We "
                         span [ ClassName "text-red-500"
                                DangerouslySetInnerHTML { __html = "&hearts; " } ] []
                         str "Open Source"
                     ]
                 Body = str loremIpsum } ]
             |> mkSections)
    ]

home |> exportDefault
