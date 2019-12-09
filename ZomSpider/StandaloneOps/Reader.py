import json

file = "../../restaurant.jl"


with open(file) as r:

    writing_file = open("new_res.jl", "a")
    for line in r:
        current = json.loads(line)
        del current["liked"]
        del current["been_there_count"]
        new_data = str(current) + "\n"
        writing_file.write(new_data)

    writing_file.close()
    r.close()