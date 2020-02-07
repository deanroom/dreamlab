package main

import "fmt"

// notifier is an interface that defines notification type behavior.
type notifier interface {
	notify()
}

// printer displays information.
type printer interface {
	print()
}

type jerry int

func (j jerry) notify() {
	fmtp.Println(j)
}

// user defines a user in the program.
type user struct {
	name  string
	email string
}

func (u user) print() {
	fmt.Printf("My name is %s and my email is %s\n", u.name, u.email)
}

func (u *user) notify() {
	fmt.Printf("Sending User Email To %s<%s>\n", u.name, u.email)
}

func (u *user) String() string {
	return fmt.Sprintf("My name is %q and my email is %q", u.name, u.email)
}

func main() {
	u := user{"Hoanh", "hoanhan@email.com"}

	sendNotification(u)

	fmt.Println(u)
	fmt.Println(&u)

	entities := []printer{
		u,
		&u,
	}

	u.name = "Hoanh An"
	u.email = "hoanhan@bennington.edu"

	for _, e := range entities {
		e.print()
	}
}

func sendNotification(n notifier) {
	n.notify()
}
