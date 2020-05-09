<<<<<<< HEAD
fn largest<T>(list: &[T]) -> T {
    let mut largest = list[0];

    for &item in list {
        if item > largest {
            largest = item;
        }
    }

    largest
}

fn main() {
    let number_list = vec![34, 50, 25, 100, 65];

    let result = largest(&number_list);
    println!("The largest number is {}", result);

    let char_list = vec!['y', 'm', 'a', 'q'];

    let result = largest(&char_list);
    println!("The largest char is {}", result);
}
=======
use std::env;
use std::process;

fn main() {
    let args: Vec<String> = env::args().collect();

    let config = hello_cargo::Config::new(&args).unwrap_or_else(|err| {
        println!("Problem parsing arguments: {}", err);
        process::exit(1);
    });

    println!("Searching for {}", config.query);
    println!("In file {}", config.filename);

    if let Err(e) = hello_cargo::run(config) {
        println!("Application error: {}", e);

        process::exit(1);
    }
}
>>>>>>> 0b3d790c537a024180f280d50e9dd0bc1b4578d8
