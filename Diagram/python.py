from plantweb.render import render

classes = [
    "Program",
    "Theme",
    "SelectScreen",
    "Form",
    "LobbyScreen",
    "Network",
    "ShipGrid",
    "ShipGridEntry",
    "MainGame",
    "$filds",
    "HitType",
    "PlayingField",
    "Tile",
    "Link",
    "FieldHandler",
    "EnemyPlayer",
    "ComputerPlayer",
    "NetworkPlayer",
]

if __name__ == '__main__':
    data = open("all.puml").readlines()
    data = data[:-1]
    for i in classes:
        data.append(f"remove {i}")
    for i in classes:
        data.append(f"restore {i}")
        with open(f"outs/out{classes.index(i)}{i}.puml", "w") as o: o.write("\n".join(data) + "\n@enduml")
        #continue
        output = render(
            "\n".join(data)+ "\n@enduml",
            engine=None,
            format='png',
            cacheopts={
                'use_cache': True
            }
        )

        print('==> OUTPUT:')
        open(f"outs/out{classes.index(i)}{i}.png", "wb").write(output[0])