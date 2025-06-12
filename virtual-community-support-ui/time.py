def check(f):
    def wrapper(h, m, *args, **kwargs):
        try:
            h = int(h)
            m = int(m)
        except ValueError:
            raise ValueError("Hour and minute must be integers")
        if not (0 <= h <= 12):
            raise ValueError("Hour must be in 0-12 format")
        if not (0 <= m < 60):
            raise ValueError("Minute must be in 0-59 format")
        return f(h, m, *args, **kwargs)
    return wrapper


@check
def time_to_angle(h,m):
    hd, md = h*30, m*5.5
    return abs(hd-md)

def angle_to_time(angle):
    pass

if __name__ == "__main__":
    ch = int(input("1.Time to angle\n2.Angle to time\nEnter choice: "))

    match ch:
        case 1:
            h = input("Enter hour: ")
            m = input("Enter minute: ")

            print(f"Angle = {time_to_angle(h,m)}")

        case 2:
            pass

        case _:
            print("Invalid choice")

