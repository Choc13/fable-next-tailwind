module Nav

open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props
open Next.Image
open Next.Link

type private MenuState =
    | Open
    | Closed

module private MenuState =
    let toggle =
        function
        | Open -> Closed
        | Closed -> Open

type NavHref =
    | Internal of string
    | External of string

let private navItem isCta (href: string) ariaLabel children =
    let aClass =
        [ "no-underline px-4 py-2 rounded-full"
          if isCta then
              "bg-secondary hover:bg-purple-light"
          else
              "hover:bg-purple-light hover:bg-opacity-50" ]
        |> String.concat " "

    if href.StartsWith('/') then
        link [ Href !^href ] [
            a
                [ AriaLabel ariaLabel
                  ClassName aClass ]
                children
        ]
    else
        a
            [ AriaLabel ariaLabel
              HTMLAttr.Href href
              ClassName aClass
              Rel "noopener" ]
            children

let View =
    FunctionComponent.Of(
        (fun () ->
            let state = Hooks.useState (Closed)

            nav [ ClassName
                      "select-none bg-primary text-white lg:flex lg:items-center w-full px-5 py-2.5 space-y-10 lg:space-y-0" ] [
                div [ ClassName "flex items-center h-12" ] [
                    link [ Href !^ "/" ] [
                        a [ AriaLabel "home"
                            ClassName "hover:bg-grey-dark p-4"
                            Rel "home" ] [
                            image [ !^(Alt "Symbolica")
                                    !^(Height !^30)
                                    !^(Width !^160)
                                    !^(Src !^ "/full-logo-30.png") ]
                        ]
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
                        navItem true "/playground" "Try Free" [ str "Try Free" ]
                    ]
                ]
            ]),
        memoizeWith = equalsButFunctions
    )
