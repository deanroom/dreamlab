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
