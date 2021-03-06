#version 120

varying vec3 v_normal;
varying vec3 v_position;

struct Material {
    vec3 diffuse;
    vec3 specular;
    vec3 emissive;
    float shininess;
};

struct DirectionalLight {
    vec3 direction;

    vec3 diffuse;
    vec3 specular;
    vec3 ambient;
};

uniform Material u_material;
uniform DirectionalLight u_directional_light;

void main()
{
    vec3 view = normalize(-v_position);
    vec3 normal = normalize(v_normal);
    
    
    vec3 light = normalize(-u_directional_light.direction);
    vec3 reflection = reflect(u_directional_light.direction, normal);
    vec3 ambient = u_directional_light.ambient * u_material.diffuse;
    vec3 diffuse = u_directional_light.diffuse * max(dot(normal, light), 0.0) * u_material.diffuse;
    vec3 specular = u_directional_light.specular * pow(max(dot(reflection, view), 0.000001), u_material.shininess) * u_material.specular;

    gl_FragColor = vec4(ambient + diffuse + specular + u_material.emissive, 1.0f);
}