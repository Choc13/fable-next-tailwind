module Nav

open Fable.React
open Fable.React.Props

type private MenuState =
    | Open
    | Closed

module private MenuState =
    let toggle =
        function
        | Open -> Closed
        | Closed -> Open

let private navItem isCta href ariaLabel children =
    a
        [ Href href
          ClassName(
              [ "no-underline px-4 py-2 rounded-full"
                if isCta then
                    "bg-secondary hover:bg-purple-light"
                else
                    "hover:bg-purple-light hover:bg-opacity-50" ]
              |> String.concat " "
          )
          Rel "noopener"
          AriaLabel ariaLabel ]
        children

let View =
    FunctionComponent.Of(
        (fun () ->
            let state = Hooks.useState (Closed)

            nav [ ClassName
                      "select-none bg-primary text-white lg:flex lg:items-center w-full px-5 py-2.5 space-y-10 lg:space-y-0" ] [
                div [ ClassName "flex items-center h-12" ] [
                    a [ Href "/"
                        Rel "home"
                        ClassName "hover:bg-grey-dark p-4"
                        AriaLabel "home" ] [
                        img [ Alt "Symbolica" // TODO: Use next/image
                              HTMLAttr.Height 30
                              HTMLAttr.Width 160
                              Src "/full-logo-30.png" ]
                    ]
                    button [ ClassName "block lg:hidden cursor-pointer ml-auto w-12 h-12"
                             OnClick(fun _ -> state.update MenuState.toggle) ] [
                        match state.current with
                        | Open -> Icons.close 40
                        | Closed -> Icons.burger 40
                    ]
                ]
                div [ ClassName(
                          [ match state.current with
                            | Open -> ""
                            | Closed -> "hidden"
                            "lg:inline-block lg:flex lg:flex-grow" ]
                          |> String.concat " "
                      ) ] [
                    div [ ClassName "flex flex-col items-start lg:flex-row lg:justify-end ml-auto gap-2" ] [
                        navItem false "https://dev.to/symbolica" "Blog" [ str "Blog" ]
                        navItem false "https://github.com/SymbolicaDev" "GitHub" [ Icons.gitHub 30 ]
                        navItem true "https://playground.symbolica.com" "Try Free" [ str "Try Free" ]
                    ]
                ]
            ]),
        memoizeWith = equalsButFunctions
    )
