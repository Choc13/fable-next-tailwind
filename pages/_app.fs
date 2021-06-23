module Pages.Index

open Fable.Core.JsInterop
open Feliz
open NextJS

importAll "${entryDir}/styles/global.css"

let footer () =
    Html.footer [ 
        prop.className "py-5 bg-primary text-white text-center py-10 space-y-10"
        prop.children [ 
            Html.div [ 
                prop.className "xl:container grid grid-flow-row lg:grid-flow-col lg:grid-cols-3 gap-10"
                prop.children [
                    Html.div [
                        prop.className "space-y-10"
                        prop.children [
                            Html.h5 "Join our mailing list"
                        ]
                    ]
                ] 
            ]
        ]
    ]

let home () =
    Html.div [ Next.Head(
                   Head.children [ Html.meta [ prop.name "title"
                                               prop.content "Symbolica" ] ]
               )
               Html.div "Fable yo Next.js"
               footer() ]

home |> exportDefault
