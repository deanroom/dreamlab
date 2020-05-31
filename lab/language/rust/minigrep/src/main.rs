use std::env;
use std::ffi::OsString;

fn main() {
    let args: Vec<String> = env::args().collect();
    let query = &args[1];
    let filename = &args[2];

    println!("Searching for {}", query);

    println!("{:?}", args);
}
