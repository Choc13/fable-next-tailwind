module Footer

open Fable.React
open Fable.React.Props

let footer () =
    footer [ ClassName "py-5 bg-primary text-white text-center py-10 space-y-10" ] [
        div [ ClassName "xl:container grid grid-flow-row lg:grid-flow-col lg:grid-cols-3 gap-10" ] [
            div [ ClassName "space-y-10" ] [
                h5 [] [ str "Join our mailing list" ]
            ]
            div [ ClassName "space-y-10" ] [
                h5 [] [ str "Contact" ]
                address [ ClassName "not-italic" ] [
                    a [ Href "mailto:dev@symbolica.dev" ] [
                        str "dev@symbolica.dev"
                    ]
                    div [] [
                        str "85 Great Portland Street"
                    ]
                    div [] [ str "First Floor" ]
                    div [] [ str "London" ]
                    div [] [ str "W1W 7LT" ]
                ]
            ]
            div [ ClassName "space-y-10 row-start-2 lg:row-auto" ] [
                h5 [] [ str "Social" ]
                div [ ClassName "grid grid-cols-8 gap-4" ] [
                    a [ Href "https://github.com/SymbolicaDev"
                        ClassName "col-start-3"
                        Rel "noopener"
                        AriaLabel "GitHub" ] [

                    ]
                ]
            ]
        ]
    ]
