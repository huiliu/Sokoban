import os

if __name__ == "__main__":
    for root, dirs, files in os.walk(".", topdown=False):
        for name in files:
            if name.endswith('sok'):
                tmp = name.rstrip('sok')+'txt'
                print(os.path.join(root, name), tmp)
                os.rename(name, tmp)
