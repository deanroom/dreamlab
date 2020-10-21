i=0
end=5000
while [ $i -le $end ]; do
    echo $i 
    curl -X GET "http://dev-webapi.rbcriyu.com/rbg/api/Test/TestCPU" -H "accept: application/json"
    i=$(($i+1))
done