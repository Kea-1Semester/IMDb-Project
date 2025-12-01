import http.client

conn = http.client.HTTPSConnection("dev-14iavcze2vf4j4gn.us.auth0.com")

payload = "{\"client_id\":\"mlEOXWMlbjrT5NjS4LTswkywq5ZEwOdR\",\"client_secret\":\"m_wYvi21t5qV_HUWlXL78i-hnVlFB3r5KPa9cjFx2b4_epe71tO2Ym-QzN2iOQSI\",\"audience\":\"https://backend-latest-hq1t.onrender.com/graphql\",\"grant_type\":\"client_credentials\"}"

headers = { 'content-type': "application/json" }

conn.request("POST", "/oauth/token", payload, headers)

res = conn.getresponse()
data = res.read()

print(data.decode("utf-8"))