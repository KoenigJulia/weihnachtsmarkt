from fastapi import FastAPI
import redis
import json
import requests

api_url = "http://localhost:5000/api/Order/price"

app = FastAPI()
r = redis.Redis(host='localhost', port=6379, db=0)


def retrieve_data(order_id: str):
    response = requests.get(f"{api_url}?orderId={order_id}")
    return response.text


@app.get("/price")
async def root(order_id: str):
    if r.exists(order_id):
        # Retrieve the value of the key from the cache
        data = json.loads(r.get(order_id))
        print(f'Retrieved data from cache: {data}')
    else:
        # Retrieve the data from the database or a web service
        data = retrieve_data(order_id)
        # Store the data in the cache
        r.set(order_id, json.dumps(data))
        # Set the expiration time for the key
        r.expire(order_id, 120)
        print(f'Retrieved data from source: {data}')
    return data
