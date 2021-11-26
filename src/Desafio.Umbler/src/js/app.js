const Request = window.Request
const Headers = window.Headers
const fetch = window.fetch

class Api {
  async request (method, url, body) {
    if (body) {
      body = JSON.stringify(body)
    }

    const request = new Request('/api/' + url, {
      method: method,
      body: body,
      credentials: 'same-origin',
      headers: new Headers({
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      })
    })

      const resp = await fetch(request)
      const jsonResult = await resp.json()

      if (!resp.ok && resp.status !== 400) {
          throw Error(resp.statusText)
      }

      if (resp.status === 400) {
        alert(jsonResult, 'success')
        //jsonResult.requestStatus = 400
    }

    return jsonResult
  }

  async getDomain (domainOrIp) {
    return this.request('GET', `domain/${domainOrIp}`)
  }
}

const api = new Api()

var callback = () => {
  const btn = document.getElementById('btn-search')
  const txt = document.getElementById('txt-search')
  const result = document.getElementById('whois-results')

  if (btn) {
      btn.onclick = async () => {

          if (txt.value === "") {
              alert('Informe um dominio valido!', 'success')
          } else {
              const response = await api.getDomain(txt.value)

              if (response) {
                  var msg = "<br/> <div class='alert alert-success' role='alert'><strong> Nome: </strong>" + response.name +
                      " <strong>Ip: </strong> " + response.ip + "<strong> Hospedado em: </strong> " + response.hostedAt + "</div>";

                  result.innerHTML = msg
              }
          }
    }
  }
}

if (document.readyState === 'complete' || (document.readyState !== 'loading' && !document.documentElement.doScroll)) {
  callback()
} else {
  document.addEventListener('DOMContentLoaded', callback)
}
